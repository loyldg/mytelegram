namespace MyTelegram.MTProto;

/// <summary>
/// Splits the client byte stream into MTProto payloads.
/// Supported transports: abridged, intermediate, padded intermediate and full, with or without obfuscation
/// (full is only accepted without obfuscation, see <see cref="FirstPacketParser"/>).
/// https://core.telegram.org/mtproto/mtproto-transports
/// </summary>
public class MtpMessageParser(
    ILogger<MtpMessageParser> logger,
    IUnencryptedMessageParser unencryptedMessageParser,
    IEncryptedMessageParser encryptedMessageParser,
    IFirstPacketParser firstPacketParser,
    IAesHelper aesHelper)
    : IMtpMessageParser, ITransientDependency
{
    /// <summary>
    /// Maximum MTProto payload length accepted from a client.
    /// </summary>
    public const int MaxPacketLength = 1024 * 1024 * 10;

    public const int FullTransportOverhead = 12; // len + seqno + crc
    public const int MaxPaddingLength = 15;

    private const int MaxHeaderLength = 8;
    private const int UnencryptedHeaderLength = 8 + 8 + 4; // auth_key_id + message_id + message_data_length
    private const int EncryptedHeaderLength = 8 + 16; // auth_key_id + msg_key
    private const int AesBlockSize = 16;

    public void ProcessFirstUnencryptedPacket(ref ReadOnlySequence<byte> buffer,
        IClientData d)
    {
        if (d.IsFirstPacketParsed)
        {
            return;
        }

        var length = (int)Math.Min(buffer.Length, FirstPacketParser.ConnectionPrefixBytes);
        Span<byte> firstPacket = stackalloc byte[FirstPacketParser.ConnectionPrefixBytes];
        firstPacket = firstPacket[..length];
        buffer.Slice(0, length).CopyTo(firstPacket);

        var data = firstPacketParser.Parse(firstPacket);
        if (data.NeedMoreData)
        {
            // TCP may deliver the connection prefix in several reads; nothing is consumed until the
            // transport can be decided.
            logger.LogDebug("[ConnectionId: {ConnectionId}] Waiting for more bytes to detect the transport, received: {Length}",
                d.ConnectionId,
                length);
            return;
        }

        if (data.ProtocolType == ProtocolType.Unknown)
        {
            throw new MtpTransportException(
                $"Unknown MTProto transport, first bytes: {Convert.ToHexString(firstPacket[..Math.Min(length, 8)])}");
        }

        logger.LogInformation("[ConnectionId: {ConnectionId}] [{ProtocolType}] protocol detected, obfuscation: {Obfuscation}, bytes: {Bytes}",
            d.ConnectionId,
            data.ProtocolType,
            data.ObfuscationEnabled,
            length);

        d.IsFirstPacketParsed = true;
        d.ObfuscationEnabled = data.ObfuscationEnabled;
        d.MtProtoType = data.ProtocolType;
        d.SendKey = data.SendKey;
        d.ReceiveKey = data.ReceiveKey;
        d.SendCount = data.SendCount;
        d.ReceiveCount = 0;
        d.ReceiveIv = data.ReceiveIv;
        d.SendIv = data.SendIv;

        buffer = buffer.Slice(data.ProtocolBufferLength);
    }

    /// <summary>
    /// Tries to read one complete frame from <paramref name="buffer"/>.
    /// Returns false when more data is needed; <paramref name="buffer"/> is then left unchanged.
    /// Throws <see cref="MtpTransportException"/> when the stream is invalid; the connection must be closed.
    /// </summary>
    public bool TryParse(ref ReadOnlySequence<byte> buffer,
        IClientData clientData,
        [NotNullWhen(true)] out IMtpMessage? message)
    {
        message = null;
        if (!clientData.IsFirstPacketParsed || buffer.IsEmpty)
        {
            return false;
        }

        var state = clientData.TransportState;
        if (!state.HasPendingHeader && !TryReadHeader(buffer, clientData, state))
        {
            return false;
        }

        var headerLength = state.PendingHeaderLength;
        var bodyLength = state.PendingBodyLength;
        var frameLength = (long)headerLength + bodyLength;
        if (buffer.Length < frameLength)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace(
                    "[ConnectionId: {ConnectionId}] Partial frame, frame length: {FrameLength}, received: {Received}",
                    clientData.ConnectionId,
                    frameLength,
                    buffer.Length);
            }

            return false;
        }

        var quickAckRequested = state.PendingQuickAckRequested;
        var memoryOwner = MemoryPool<byte>.Shared.Rent(bodyLength);
        try
        {
            var body = memoryOwner.Memory[..bodyLength];
            buffer.Slice(headerLength, bodyLength).CopyTo(body.Span);
            Deobfuscate(body.Span, clientData, clientData.SendCount + (ulong)headerLength);

            // The whole frame is consumed: advance the obfuscation counter exactly once.
            if (clientData.ObfuscationEnabled)
            {
                clientData.SendCount += (ulong)frameLength;
            }

            buffer = buffer.Slice(frameLength);

            var payload = GetPayload(body, clientData, state);
            state.ClearPendingHeader();

            message = CreateMessage(payload, clientData, quickAckRequested);
            message.MemoryOwner = memoryOwner;

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace(
                    "[ConnectionId: {ConnectionId}] [{ProtocolType}] Frame received, frame length: {FrameLength}, payload length: {PayloadLength}, quick ack: {QuickAck}",
                    clientData.ConnectionId,
                    clientData.MtProtoType,
                    frameLength,
                    payload.Length,
                    quickAckRequested);
            }

            return true;
        }
        catch
        {
            memoryOwner.Dispose();
            message = null;
            throw;
        }
    }

    private bool TryReadHeader(in ReadOnlySequence<byte> buffer,
        IClientData d,
        MtpTransportState state)
    {
        return d.MtProtoType switch
        {
            ProtocolType.Abridge => TryReadAbridgedHeader(buffer, d, state),
            ProtocolType.Intermediate => TryReadIntermediateHeader(buffer, d, state, false),
            ProtocolType.PaddedIntermediate => TryReadIntermediateHeader(buffer, d, state, true),
            ProtocolType.Full => TryReadFullHeader(buffer, d, state),
            _ => throw new MtpTransportException($"Unsupported MTProto transport: {d.MtProtoType}")
        };
    }

    private bool TryReadAbridgedHeader(in ReadOnlySequence<byte> buffer,
        IClientData d,
        MtpTransportState state)
    {
        /*
         * +-+----...----+        +-+---+----...----+
         * |l|  payload  |   OR   |h|len|  payload  |
         * +-+----...----+        +-+---+----...----+
         * l   = payload length / 4 (< 0x7f)
         * h   = 0x7f, len = payload length / 4 in 3 bytes (little endian)
         * Quick ACK request: the most significant bit of l is set, or h = 0xff.
         */
        Span<byte> header = stackalloc byte[4];
        if (!TryPeekHeader(buffer, d, header[..1]))
        {
            return false;
        }

        var quickAckRequested = (header[0] & 0x80) != 0;
        var shortLength = header[0] & 0x7f;
        int headerLength;
        int lengthInInts;
        if (shortLength == 0x7f)
        {
            if (!TryPeekHeader(buffer, d, header))
            {
                return false;
            }

            headerLength = 4;
            lengthInInts = header[1] | (header[2] << 8) | (header[3] << 16);
        }
        else
        {
            headerLength = 1;
            lengthInInts = shortLength;
        }

        var bodyLength = (long)lengthInInts * 4;
        ValidateBodyLength(bodyLength, MaxPacketLength, d);
        state.SetPendingHeader(headerLength, (int)bodyLength, quickAckRequested, 0, 0);
        LogQuickAckRequestedIfNeeded(d, quickAckRequested);

        return true;
    }

    private bool TryReadIntermediateHeader(in ReadOnlySequence<byte> buffer,
        IClientData d,
        MtpTransportState state,
        bool withPadding)
    {
        /*
         * Intermediate:        +----+----...----+
         *                      |len.|  payload  |
         *                      +----+----...----+
         * Padded intermediate: +----+----...----+----...----+
         *                      |tlen|  payload  |  padding  |   (padding: 0-15 random bytes)
         *                      +----+----...----+----...----+
         * Quick ACK request: the most significant bit of len/tlen is set.
         */
        Span<byte> header = stackalloc byte[4];
        if (!TryPeekHeader(buffer, d, header))
        {
            return false;
        }

        var rawLength = BinaryPrimitives.ReadUInt32LittleEndian(header);
        var quickAckRequested = (rawLength & MtpQuickAckTracker.TokenFlag) != 0;
        var bodyLength = (long)(rawLength & ~MtpQuickAckTracker.TokenFlag);
        ValidateBodyLength(bodyLength, withPadding ? MaxPacketLength + MaxPaddingLength : MaxPacketLength, d);
        state.SetPendingHeader(4, (int)bodyLength, quickAckRequested, 0, 0);
        LogQuickAckRequestedIfNeeded(d, quickAckRequested);

        return true;
    }

    private bool TryReadFullHeader(in ReadOnlySequence<byte> buffer,
        IClientData d,
        MtpTransportState state)
    {
        /*
         * +----+----+----...----+----+
         * |len.|seq.|  payload  |crc.|
         * +----+----+----...----+----+
         * len includes len, seq, payload and crc. seq starts at 0 and is incremented for every frame.
         * crc = CRC32(len + seq + payload).
         * The documentation does not define quick ACK for this transport, so a length with the most
         * significant bit set is rejected as invalid.
         */
        Span<byte> header = stackalloc byte[MaxHeaderLength];
        if (!TryPeekHeader(buffer, d, header))
        {
            return false;
        }

        var lengthField = BinaryPrimitives.ReadInt32LittleEndian(header);
        var seqNo = BinaryPrimitives.ReadInt32LittleEndian(header[4..]);
        if (lengthField <= FullTransportOverhead || lengthField > MaxPacketLength + FullTransportOverhead)
        {
            throw CreateInvalidFrameException(d, $"invalid full transport length: {lengthField}");
        }

        if (seqNo != state.InboundSeqNo)
        {
            throw CreateInvalidFrameException(d, $"unexpected full transport seqno: {seqNo}, expected: {state.InboundSeqNo}");
        }

        state.SetPendingHeader(MaxHeaderLength, lengthField - MaxHeaderLength, false, lengthField, seqNo);

        return true;
    }

    /// <summary>
    /// Copies the first bytes of <paramref name="buffer"/> and de-obfuscates them without advancing the
    /// obfuscation counter (AES-CTR can be computed at any offset).
    /// </summary>
    private bool TryPeekHeader(in ReadOnlySequence<byte> buffer,
        IClientData d,
        Span<byte> header)
    {
        if (buffer.Length < header.Length)
        {
            return false;
        }

        buffer.Slice(0, header.Length).CopyTo(header);
        Deobfuscate(header, d, d.SendCount);

        return true;
    }

    private ReadOnlyMemory<byte> GetPayload(Memory<byte> body,
        IClientData d,
        MtpTransportState state)
    {
        switch (d.MtProtoType)
        {
            case ProtocolType.Full:
                {
                    var payloadLength = body.Length - 4;
                    var span = body.Span;
                    var expectedCrc = BinaryPrimitives.ReadUInt32LittleEndian(span[payloadLength..]);
                    Span<byte> header = stackalloc byte[MaxHeaderLength];
                    BinaryPrimitives.WriteInt32LittleEndian(header, state.PendingFullLengthField);
                    BinaryPrimitives.WriteInt32LittleEndian(header[4..], state.PendingFullSeqNo);
                    var crc = Crc32.Append(Crc32.Compute(header), span[..payloadLength]);
                    if (crc != expectedCrc)
                    {
                        throw CreateInvalidFrameException(d,
                            $"full transport crc mismatch, expected: {expectedCrc:x8}, actual: {crc:x8}, seqno: {state.PendingFullSeqNo}");
                    }

                    state.InboundSeqNo = unchecked(state.InboundSeqNo + 1);

                    return body[..payloadLength];
                }
            case ProtocolType.PaddedIntermediate:
                return RemovePadding(body, d);
            default:
                return body;
        }
    }

    /// <summary>
    /// Removes the 0-15 random padding bytes of the padded intermediate transport, using the length
    /// information of the MTProto payload itself:
    /// unencrypted: auth_key_id + message_id + message_data_length + message_data;
    /// encrypted: auth_key_id + msg_key + encrypted_data (a multiple of 16 bytes, like tdlib which drops
    /// the non-aligned tail).
    /// </summary>
    private ReadOnlyMemory<byte> RemovePadding(Memory<byte> body,
        IClientData d)
    {
        var span = body.Span;
        if (span.Length < 8)
        {
            throw CreateInvalidFrameException(d, $"padded payload is too short: {span.Length}");
        }

        int payloadLength;
        if (BinaryPrimitives.ReadInt64LittleEndian(span) == 0)
        {
            if (span.Length < UnencryptedHeaderLength)
            {
                throw CreateInvalidFrameException(d, $"unencrypted payload is too short: {span.Length}");
            }

            var messageDataLength = BinaryPrimitives.ReadInt32LittleEndian(span.Slice(16, 4));
            if (messageDataLength < 0 || messageDataLength > span.Length - UnencryptedHeaderLength)
            {
                throw CreateInvalidFrameException(d,
                    $"invalid unencrypted message_data_length: {messageDataLength}, available: {span.Length - UnencryptedHeaderLength}");
            }

            payloadLength = UnencryptedHeaderLength + messageDataLength;
        }
        else
        {
            if (span.Length < EncryptedHeaderLength + AesBlockSize)
            {
                throw CreateInvalidFrameException(d, $"encrypted payload is too short: {span.Length}");
            }

            payloadLength = EncryptedHeaderLength + ((span.Length - EncryptedHeaderLength) & ~(AesBlockSize - 1));
        }

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("[ConnectionId: {ConnectionId}] Padding removed: {PaddingLength} bytes",
                d.ConnectionId,
                span.Length - payloadLength);
        }

        return body[..payloadLength];
    }

    private IMtpMessage CreateMessage(ReadOnlyMemory<byte> payload,
        IClientData d,
        bool quickAckRequested)
    {
        var span = payload.Span;
        if (span.Length < 8)
        {
            throw CreateInvalidFrameException(d, $"payload is too short: {span.Length}");
        }

        if (BinaryPrimitives.ReadInt64LittleEndian(span) == 0)
        {
            if (span.Length < UnencryptedHeaderLength + 4)
            {
                throw CreateInvalidFrameException(d, $"unencrypted payload is too short: {span.Length}");
            }

            var messageDataLength = BinaryPrimitives.ReadInt32LittleEndian(span.Slice(16, 4));
            if (messageDataLength < 4 || messageDataLength > span.Length - UnencryptedHeaderLength)
            {
                throw CreateInvalidFrameException(d,
                    $"invalid unencrypted message_data_length: {messageDataLength}, available: {span.Length - UnencryptedHeaderLength}");
            }

            if (quickAckRequested)
            {
                // Quick ACK is only defined for encrypted payloads.
                logger.LogDebug("[ConnectionId: {ConnectionId}] Quick ack requested for an unencrypted payload, ignored",
                    d.ConnectionId);
            }

            return unencryptedMessageParser.Parse(payload);
        }

        var encryptedDataLength = span.Length - EncryptedHeaderLength;
        if (encryptedDataLength < AesBlockSize || encryptedDataLength % AesBlockSize != 0)
        {
            throw CreateInvalidFrameException(d, $"invalid encrypted data length: {encryptedDataLength}");
        }

        var encryptedMessage = encryptedMessageParser.Parse(payload);
        encryptedMessage.QuickAckRequested = quickAckRequested;

        return encryptedMessage;
    }

    private void ValidateBodyLength(long bodyLength,
        int maxLength,
        IClientData d)
    {
        if (bodyLength <= 0 || bodyLength > maxLength)
        {
            throw CreateInvalidFrameException(d, $"invalid frame length: {bodyLength}, max: {maxLength}");
        }
    }

    private void LogQuickAckRequestedIfNeeded(IClientData d,
        bool quickAckRequested)
    {
        if (quickAckRequested && logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("[ConnectionId: {ConnectionId}] [{ProtocolType}] Quick ack requested",
                d.ConnectionId,
                d.MtProtoType);
        }
    }

    private MtpTransportException CreateInvalidFrameException(IClientData d,
        string reason)
    {
        logger.LogWarning("[ConnectionId: {ConnectionId}] [{ProtocolType}] Invalid MTProto transport frame: {Reason}",
            d.ConnectionId,
            d.MtProtoType,
            reason);

        return new MtpTransportException($"[{d.MtProtoType}] {reason}");
    }

    private void Deobfuscate(Span<byte> data,
        IClientData d,
        ulong offset)
    {
        if (d.ObfuscationEnabled && !data.IsEmpty)
        {
            aesHelper.CtrEncrypt(data, data, d.SendKey, d.SendIv, offset);
        }
    }
}
