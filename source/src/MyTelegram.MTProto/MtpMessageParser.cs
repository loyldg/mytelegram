namespace MyTelegram.MTProto;

public class MtpMessageParser(
    ILogger<MtpMessageParser> logger,
    IUnencryptedMessageParser unencryptedMessageParser,
    IEncryptedMessageParser encryptedMessageParser,
    IFirstPacketParser firstPacketParser,
    IAesHelper aesHelper)
    : IMtpMessageParser, ITransientDependency
{
    private const int ConnectionStartPrefixSize = 64;
    private const int MaxPacketLength = 1024 * 1024 * 10;
    private const int UnObfuscationFirstPacketLength = 4;

    public void ProcessFirstUnencryptedPacket(ref ReadOnlySequence<byte> buffer,
        IClientData d)
    {
        var length = Math.Min((int)buffer.Length, ConnectionStartPrefixSize);
        Span<byte> firstPacket = stackalloc byte[length];
        buffer.Slice(0, length).CopyTo(firstPacket);

        var firstPackData = ProcessFirstUnencryptedPacket(firstPacket, d);
        buffer = buffer.Slice(firstPackData.ProtocolBufferLength);
    }

    public bool TryParse(ref ReadOnlySequence<byte> buffer,
        IClientData clientData,
        [NotNullWhen(true)] out IMtpMessage? message)
    {
        if (TryParseData(ref buffer, clientData, out var data))
        {
            var length = (int)data.Length;
            var memoryOwner = MemoryPool<byte>.Shared.Rent(length);
            var tempMemory = memoryOwner.Memory[..length];

            var span = tempMemory.Span;
            data.CopyTo(span);


            DecryptBytes(span[..length], clientData);
            var decryptedMemory = tempMemory;
            var authKeyId = BinaryPrimitives.ReadInt64LittleEndian(span);
            if (authKeyId == 0)
            {
                message = unencryptedMessageParser.Parse(decryptedMemory);
            }
            else
            {
                message = encryptedMessageParser.Parse(decryptedMemory);
            }
            message.MemoryOwner = memoryOwner;

            return true;

        }

        message = default;
        return false;
    }

    private void DecryptBytes(Span<byte> encryptedBytes,
        IClientData d)
    {
        if (d.ObfuscationEnabled)
        {
            aesHelper.CtrEncrypt(encryptedBytes, encryptedBytes, d.SendKey, d.SendIv, d.SendCount);

            d.SendCount += (uint)encryptedBytes.Length;
        }
    }

    private int GetAbridgedPacketLength( /*ReadOnlySpan<byte> data*/ /*SequenceReader<byte> reader,*/
        in ReadOnlySequence<byte> data,
        IClientData d,
        out int skipCount
    )
    {
        /*
        * Abridged
          The lightest protocol available.

          Overhead: Very small
          Minimum envelope length: 1 byte
          Maximum envelope length: 4 bytes
          Payload structure:

          +-+----...----+
          |l|  payload  |
          +-+----...----+
          OR

          +-+---+----...----+
          |h|len|  payload  +
          +-+---+----...----+
          Before sending anything into the underlying socket (see transports), the client must first send 0xef as the first byte (the server will not send 0xef as the first byte in the first reply).
          Then, payloads are wrapped in the following envelope:

          Length: payload length, divided by four, and encoded as a single byte, only if the resulting packet length is a value between 0x01..0x7e.
          Payload: the MTProto payload
          If the packet length divided by four is bigger than or equal to 127 (>= 0x7f), the following envelope must be used, instead:

          Header: A single byte of value 0x7f
          Length: payload length, divided by four, and encoded as 3 length bytes (little endian)
          Payload: the MTProto payload
        */
        const int maxFirstByteValue = 0x7f;
        Span<byte> firstBytes = [data.FirstSpan[0]]; //data.First[..1];
        DecryptBytes(firstBytes, d);
        var firstByte = firstBytes[0] & maxFirstByteValue;

        int packetLength;
        if (firstByte < maxFirstByteValue)
        {
            packetLength = firstByte * 4; // + 1;
            skipCount = 1;
        }
        else if (firstByte == maxFirstByteValue)
        {
            Span<byte> lengthBytes = stackalloc byte[3];
            data.Slice(1, 3).CopyTo(lengthBytes);
            DecryptBytes(lengthBytes, d);
            packetLength = (lengthBytes[0] | (lengthBytes[1] << 8) |
                            (lengthBytes[2] << 16)) * 4; // + 4;
            skipCount = 4;
        }
        else
        {
            logger.LogWarning(
                "[ConnectionId: {ConnectionId}] Invalid packet, length is greater than {MaxFirstByte}, but first byte is not {MaxFirstByte}, first byte: {FirstByte}",
                d.ConnectionId,
                maxFirstByteValue,
                maxFirstByteValue,
                firstByte
            );
            throw new ArgumentException($"Invalid packet, first byte: {firstByte}");
        }

        return packetLength;
    }

    private int GetIntermediatePacketLength( /*ReadOnlySpan<byte> data,*/ /*SequenceReader<byte> reader,*/
        in ReadOnlySequence<byte> data,
        IClientData d,
        out int skipCount
    )
    {
        /*
         * In case 4-byte data alignment is needed, an intermediate version of the original protocol may be used.

           Overhead: small
           Minimum envelope length: 4 bytes
           Maximum envelope length: 4 bytes
           Payload structure:
           
           +----+----...----+
           +len.+  payload  +
           +----+----...----+
           Before sending anything into the underlying socket (see transports), the client must first send 0xeeeeeeee as the first int (four bytes, the server will not send 0xeeeeeeee as the first int in the first reply).
           Then, payloads are wrapped in the following envelope:
           
           Length: payload length encoded as 4 length bytes (little endian)
           Payload: the MTProto payload
         */
        Span<byte> lengthBytes = [data.FirstSpan[0], data.FirstSpan[1], data.FirstSpan[2], data.FirstSpan[3]];
        DecryptBytes(lengthBytes, d);
        var packetLength = BinaryPrimitives.ReadInt32LittleEndian(lengthBytes);

        skipCount = 4;
        return packetLength;
    }

    private int GetPacketLength( /*ReadOnlySpan<byte> data*/ /*SequenceReader<byte> reader, */
        in ReadOnlySequence<byte> data,
        IClientData d,
        out int skipCount
    )
    {
        if (d.CurrentPacketLength > 0)
        {
            skipCount = d.SkipCount;
            return d.CurrentPacketLength;
        }

        return d.MtProtoType switch
        {
            ProtocolType.Abridge => GetAbridgedPacketLength(data, d, out skipCount),
            ProtocolType.Intermediate => GetIntermediatePacketLength(data, d, out skipCount),
            _ => throw new NotSupportedException($"Not supported protocol:{d.MtProtoType}")
        };
    }

    public FirstPacketData ProcessFirstUnencryptedPacket(ReadOnlySpan<byte> buffer,
        IClientData d)
    {
        var data = firstPacketParser.Parse(buffer);
        if (data.ProtocolType != ProtocolType.Unknown)
        {
            logger.LogInformation("[{ConnectionId}] [{ProtocolType}] protocol detected, bytes: {Bytes}", d.ConnectionId, data.ProtocolType, buffer.Length);
        }

        d.IsFirstPacketParsed = true;
        d.ObfuscationEnabled = data.ObfuscationEnabled;
        d.MtProtoType = data.ProtocolType;
        d.SendKey = data.SendKey;
        d.ReceiveKey = data.ReceiveKey;
        d.SendCount = data.SendCount;
        d.ReceiveIv = data.ReceiveIv;
        d.SendIv = data.SendIv;

        return data;
    }

    private bool TryParseData(ref ReadOnlySequence<byte> buffer,
        IClientData clientData,
        out ReadOnlySequence<byte> data)
    {
        var reader = new SequenceReader<byte>(buffer);

        if (!clientData.IsFirstPacketParsed)
        {
            data = reader.Remaining switch
            {
                UnObfuscationFirstPacketLength => reader.UnreadSequence.Slice(0, UnObfuscationFirstPacketLength),
                >= ConnectionStartPrefixSize => reader.UnreadSequence.Slice(0, ConnectionStartPrefixSize),
                _ => throw new ArgumentException($"Invalid first packet size: {reader.Remaining}")
            };
            buffer = reader.UnreadSequence.Slice(data.End);
            return true;
        }

        var packetLength = GetPacketLength(buffer, clientData, out var skipCount);
        if (packetLength > MaxPacketLength)
        {
            logger.LogWarning(
                "[ConnectionId: {ConnectionId}] Packet length is greater than the max value({MaxPacketLength})",
                clientData.ConnectionId,
                MaxPacketLength);

            data = default;
            return false;
        }

        if (reader.Remaining < packetLength)
        {
            logger.LogDebug(
                "[ConnectionId: {ConnectionId}] Packet length is {ActualLength}, remaining is {Remaining} need more data({MoreDataBytes})",
                clientData.ConnectionId,
                packetLength,
                reader.Remaining,
                packetLength - reader.Remaining);
            clientData.CurrentPacketLength = packetLength;
            clientData.SkipCount = skipCount;
            data = default;

            return false;
        }

        if (clientData.CurrentPacketLength > 0)
        {
            clientData.CurrentPacketLength = 0;
            clientData.SkipCount = 0;
        }

        data = reader.UnreadSequence.Slice(skipCount, packetLength);
        buffer = reader.UnreadSequence.Slice(data.End);
        logger.LogTrace("[ConnectionId: {ConnectionId}] Packet length is {PacketLength}",
            clientData.ConnectionId,
            packetLength);

        return true;
    }
}
