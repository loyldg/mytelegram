namespace MyTelegram.MTProto;

public class MtpMessageEncoder : IMtpMessageEncoder
{
    private static readonly byte[] DefaultAuthKeyIdBytes = new byte[8];
    private readonly IAesHelper _aesHelper;
    private readonly IMessageIdHelper _messageIdHelper;

    public MtpMessageEncoder(IAesHelper aesHelper,
        IMessageIdHelper messageIdHelper)
    {
        _aesHelper = aesHelper;
        _messageIdHelper = messageIdHelper;
    }

    public int Encode(IClientData d,
        EncryptedMessageResponse m,
        Span<byte> encodedBytes)
    {
        return d.MtProtoType switch
        {
            ProtocolType.Abridge => EncodeToProtocolAbridgedBytes(d, m, encodedBytes),
            ProtocolType.Intermediate => EncodeToProtocolIntermediateBytes(d, m, encodedBytes),
            _ => throw new NotImplementedException($"Not allowed MTProtocol type:{d.MtProtoType}")
        };
    }

    public int Encode(IClientData d,
        UnencryptedMessageResponse m,
        Span<byte> encodedBytes)
    {
        return d.MtProtoType switch
        {
            ProtocolType.Abridge => EncodeToProtocolAbridgedBytes(d, m, encodedBytes),
            ProtocolType.Intermediate => EncodeToProtocolIntermediateBytes(d, m, encodedBytes),
            _ => throw new NotImplementedException($"Not allowed MTProtocol type:{d.MtProtoType}")
        };
    }

    public Memory<byte> Encode(IClientData d,
        EncryptedMessageResponse m)
    {
        throw new NotImplementedException();
    }

    public Memory<byte> Encode(IClientData d,
        UnencryptedMessageResponse m)
    {
        throw new NotImplementedException();
    }

    #region Protocol Abridged

    private int EncodeToProtocolAbridgedBytes(IClientData d,
        UnencryptedMessageResponse message,
        Span<byte> encodedBytes)
    {
        var messageIdBytes = BitConverter.GetBytes(_messageIdHelper.GenerateMessageId());
        var messageDataLengthBytes = BitConverter.GetBytes(message.Data.Length);
        var totalCount = EncodeToProtocolAbridgedBytesCore(encodedBytes,
            DefaultAuthKeyIdBytes,
            messageIdBytes,
            messageDataLengthBytes,
            message.Data);

        _aesHelper.Ctr128Encrypt(encodedBytes[..totalCount], d.ReceiveKey, d.ReceiveCtrState);

        return totalCount;
    }

    private int EncodeToProtocolAbridgedBytes(IClientData d,
        EncryptedMessageResponse message,
        Span<byte> encodedBytes)
    {
        var totalCount = EncodeToProtocolAbridgedBytesCore(encodedBytes, message.Data);
        if (d.ObfuscationEnabled)
        {
            _aesHelper.Ctr128Encrypt(encodedBytes[..totalCount], d.ReceiveKey, d.ReceiveCtrState);

            return totalCount;
        }

        return totalCount;
    }

    private int EncodeToProtocolAbridgedBytesCore(Span<byte> encodedBytes,
        params byte[][] bufferList)
    {
        // https://corefork.telegram.org/mtproto/mtproto-transports#abridged
        /*
         *
        +-+----...----+
        |l|  payload  |
        +-+----...----+
        OR
        +-+---+----...----+
        |h|len|  payload  +
        +-+---+----...----+
         *
         */
        var length = bufferList.Sum(p => p.Length);
        var intSize = length / 4;

        var lengthBytes = intSize < 0x7f ? new[] { (byte)intSize } : BitConverter.GetBytes(((intSize << 8) + 4) | 0x7f);
        var totalLength = lengthBytes.Length + length;
        lengthBytes.CopyTo(encodedBytes);
        var offset = lengthBytes.Length;

        foreach (var bytes in bufferList)
        {
            bytes.CopyTo(encodedBytes[offset..]);
            offset += bytes.Length;
        }

        return totalLength;
    }

    #endregion

    #region Protocol Intermediate

    private int EncodeToProtocolIntermediateBytes(IClientData d,
        UnencryptedMessageResponse message,
        Span<byte> encodedBytes)
    {
        var messageIdBytes = BitConverter.GetBytes(_messageIdHelper.GenerateMessageId());
        var messageDataLengthBytes = BitConverter.GetBytes(message.Data.Length);
        var totalCount = EncodeToProtocolIntermediateBytesCore(encodedBytes,
            DefaultAuthKeyIdBytes,
            messageIdBytes,
            messageDataLengthBytes,
            message.Data);

        if (d.ObfuscationEnabled)
        {
            _aesHelper.Ctr128Encrypt(encodedBytes[..totalCount], d.ReceiveKey, d.ReceiveCtrState);
        }

        return totalCount;
    }

    private int EncodeToProtocolIntermediateBytes(IClientData d,
        EncryptedMessageResponse message,
        Span<byte> encodedBytes)
    {
        var totalCount = EncodeToProtocolIntermediateBytesCore(encodedBytes, message.Data);
        if (d.ObfuscationEnabled)
        {
            _aesHelper.Ctr128Encrypt(encodedBytes[..totalCount], d.ReceiveKey, d.ReceiveCtrState);
        }

        return totalCount;
    }

    private int EncodeToProtocolIntermediateBytesCore(Span<byte> encodedBytes,
        params byte[][] bufferList)
    {
        // https://corefork.telegram.org/mtproto/mtproto-transports#intermediate
        /*
         * +----+----...----+
            +len.+  payload  +
            +----+----...----+
         */
        var length = bufferList.Sum(p => p.Length);
        var lengthBytes = BitConverter.GetBytes(length);
        var totalLength = lengthBytes.Length + length;
        lengthBytes.CopyTo(encodedBytes);
        var offset = lengthBytes.Length;

        foreach (var bytes in bufferList)
        {
            bytes.CopyTo(encodedBytes[offset..]);
            offset += bytes.Length;
        }

        return totalLength;
    }

    #endregion
}
