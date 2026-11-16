namespace MyTelegram.MTProto;

/// <summary>
/// Wraps MTProto payloads in the transport envelope of the connection and applies obfuscation.
/// Encoding changes per-connection state (obfuscation counter, full transport seqno), so calls for one
/// connection must be serialized and the encoded bytes must be written in the same order.
/// https://core.telegram.org/mtproto/mtproto-transports
/// </summary>
public class MtpMessageEncoder(
    IAesHelper aesHelper,
    IMessageIdHelper messageIdHelper,
    ILogger<MtpMessageEncoder> logger)
    : IMtpMessageEncoder, ITransientDependency
{
    /// <summary>
    /// Maximum transport overhead added to a payload: padded intermediate (4 + 15) is the largest,
    /// full is 12, intermediate 4, abridged 1 or 4.
    /// </summary>
    public const int MaxTransportOverhead = 4 + MtpMessageParser.MaxPaddingLength;

    /// <summary>
    /// Maximum length of an encoded quick ACK (padded intermediate: tlen + 0xffffffff + token + 8 bytes padding).
    /// </summary>
    public const int MaxQuickAckLength = 4 + 4 + 4 + 8;

    private const int UnencryptedHeaderLength = 8 + 8 + 4;
    private const int MaxAbridgedLengthInInts = 0xFFFFFF;
    private const int MinEncryptedPayloadLength = 16;

    public int Encode(IClientData d,
        EncryptedMessageResponse m,
        Span<byte> encodedBytes)
    {
        var payloadLength = m.Data.Length;
        var payload = WriteHeader(d, payloadLength, encodedBytes, out var headerLength);
        m.Data.Span.CopyTo(payload);

        return CompleteFrame(d, encodedBytes, headerLength, payloadLength);
    }

    public int Encode(IClientData d,
        UnencryptedMessageResponse m,
        Span<byte> encodedBytes)
    {
        // auth_key_id (0) + message_id + message_data_length + message_data
        var payloadLength = UnencryptedHeaderLength + m.Data.Length;
        var payload = WriteHeader(d, payloadLength, encodedBytes, out var headerLength);
        BinaryPrimitives.WriteInt64LittleEndian(payload, 0);
        BinaryPrimitives.WriteInt64LittleEndian(payload[8..], messageIdHelper.GenerateMessageId());
        BinaryPrimitives.WriteInt32LittleEndian(payload[16..], m.Data.Length);
        m.Data.Span.CopyTo(payload[UnencryptedHeaderLength..]);

        return CompleteFrame(d, encodedBytes, headerLength, payloadLength);
    }

    public int EncodeQuickAck(IClientData d,
        uint quickAckToken,
        Span<byte> encodedBytes)
    {
        int length;
        switch (d.MtProtoType)
        {
            case ProtocolType.Abridge:
                // Standalone 4-byte packet without length header, bswapped: the first byte has the MSB set,
                // which never happens for a normal abridged packet sent by the server (length <= 0x7f).
                BinaryPrimitives.WriteUInt32BigEndian(encodedBytes, quickAckToken);
                length = 4;
                break;
            case ProtocolType.Intermediate:
                // Standalone 4-byte packet without length header; as a little-endian int it is >= 0x80000000.
                BinaryPrimitives.WriteUInt32LittleEndian(encodedBytes, quickAckToken);
                length = 4;
                break;
            case ProtocolType.PaddedIntermediate:
                {
                    // tlen | 0xffffffff | token | 0-8 random padding bytes (tlen is 8-16).
                    var paddingLength = RandomNumberGenerator.GetInt32(9);
                    var tlen = 8 + paddingLength;
                    BinaryPrimitives.WriteInt32LittleEndian(encodedBytes, tlen);
                    BinaryPrimitives.WriteUInt32LittleEndian(encodedBytes[4..], 0xFFFFFFFF);
                    BinaryPrimitives.WriteUInt32LittleEndian(encodedBytes[8..], quickAckToken);
                    RandomNumberGenerator.Fill(encodedBytes.Slice(12, paddingLength));
                    length = 4 + tlen;
                    break;
                }
            default:
                // Quick ACK is not defined for the full transport.
                logger.LogDebug("[ConnectionId: {ConnectionId}] Quick ack is not supported by transport {ProtocolType}",
                    d.ConnectionId,
                    d.MtProtoType);
                return 0;
        }

        Obfuscate(d, encodedBytes[..length]);

        return length;
    }

    /// <summary>
    /// Writes the transport header and returns the span where the payload must be written.
    /// </summary>
    private static Span<byte> WriteHeader(IClientData d,
        int payloadLength,
        Span<byte> encodedBytes,
        out int headerLength)
    {
        switch (d.MtProtoType)
        {
            case ProtocolType.Abridge:
                {
                    if (payloadLength % 4 != 0)
                    {
                        throw new ArgumentException($"Abridged payload length must be a multiple of 4: {payloadLength}");
                    }

                    var lengthInInts = payloadLength / 4;
                    if (lengthInInts < 0x7f)
                    {
                        encodedBytes[0] = (byte)lengthInInts;
                        headerLength = 1;
                    }
                    else
                    {
                        if (lengthInInts > MaxAbridgedLengthInInts)
                        {
                            throw new ArgumentException($"Abridged payload is too long: {payloadLength}");
                        }

                        encodedBytes[0] = 0x7f;
                        encodedBytes[1] = (byte)lengthInInts;
                        encodedBytes[2] = (byte)(lengthInInts >> 8);
                        encodedBytes[3] = (byte)(lengthInInts >> 16);
                        headerLength = 4;
                    }

                    break;
                }
            case ProtocolType.Intermediate:
            case ProtocolType.PaddedIntermediate:
                // The length (tlen for padded) is written in CompleteFrame, when the padding is known.
                headerLength = 4;
                break;
            case ProtocolType.Full:
                // len and seqno are written in CompleteFrame.
                headerLength = 8;
                break;
            default:
                throw new NotSupportedException($"Not allowed MTProtocol type:{d.MtProtoType}");
        }

        return encodedBytes.Slice(headerLength, payloadLength);
    }

    private int CompleteFrame(IClientData d,
        Span<byte> encodedBytes,
        int headerLength,
        int payloadLength)
    {
        var totalLength = headerLength + payloadLength;
        switch (d.MtProtoType)
        {
            case ProtocolType.Intermediate:
                BinaryPrimitives.WriteInt32LittleEndian(encodedBytes, payloadLength);
                break;
            case ProtocolType.PaddedIntermediate:
                {
                    // Payloads shorter than 16 bytes are transport errors (a 4-byte negative code).
                    // Clients (tdlib Transport::read) only treat a packet as an error/quick ACK when it is
                    // shorter than 16 bytes, so the padding must keep tlen below 16 in that case.
                    var maxPaddingLength = payloadLength < MinEncryptedPayloadLength
                        ? Math.Min(MtpMessageParser.MaxPaddingLength, MinEncryptedPayloadLength - 1 - payloadLength)
                        : MtpMessageParser.MaxPaddingLength;
                    var paddingLength = RandomNumberGenerator.GetInt32(maxPaddingLength + 1);
                    RandomNumberGenerator.Fill(encodedBytes.Slice(totalLength, paddingLength));
                    BinaryPrimitives.WriteInt32LittleEndian(encodedBytes, payloadLength + paddingLength);
                    totalLength += paddingLength;
                    break;
                }
            case ProtocolType.Full:
                {
                    var state = d.TransportState;
                    var seqNo = state.OutboundSeqNo;
                    state.OutboundSeqNo = unchecked(seqNo + 1);
                    var lengthField = totalLength + 4;
                    BinaryPrimitives.WriteInt32LittleEndian(encodedBytes, lengthField);
                    BinaryPrimitives.WriteInt32LittleEndian(encodedBytes[4..], seqNo);
                    var crc = Crc32.Compute(encodedBytes[..totalLength]);
                    BinaryPrimitives.WriteUInt32LittleEndian(encodedBytes[totalLength..], crc);
                    totalLength += 4;
                    break;
                }
        }

        Obfuscate(d, encodedBytes[..totalLength]);

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("[ConnectionId: {ConnectionId}] [{ProtocolType}] Frame encoded, payload length: {PayloadLength}, frame length: {FrameLength}",
                d.ConnectionId,
                d.MtProtoType,
                payloadLength,
                totalLength);
        }

        return totalLength;
    }

    private void Obfuscate(IClientData d,
        Span<byte> frame)
    {
        if (d.ObfuscationEnabled)
        {
            aesHelper.CtrEncrypt(frame, frame, d.ReceiveKey, d.ReceiveIv, d.ReceiveCount);
            d.ReceiveCount += (ulong)frame.Length;
        }
    }
}
