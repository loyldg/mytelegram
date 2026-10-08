namespace MyTelegram.MTProto;

public class FirstPacketParser(
    ILogger<FirstPacketParser> logger,
    IAesHelper aesHelper)
    : IFirstPacketParser, ITransientDependency
{
    private const byte AbridgedFlag = 0xef;
    private const byte IntermediateFlag = 0xee;
    private const int ConnectionPrefixBytes = 64;
    private static readonly byte[] AbridgedBytes = [AbridgedFlag, AbridgedFlag, AbridgedFlag, AbridgedFlag];

    private static readonly byte[] IntermediateBytes =
        [IntermediateFlag, IntermediateFlag, IntermediateFlag, IntermediateFlag];

    public FirstPacketData Parse(ReadOnlySpan<byte> firstPacket)
    {
        //if (firstPacket.Length == 4 || firstPacket.Length == 1)
        //{
        //    return ParseUnObfuscationFirstPacket(ref firstPacket);
        //}

        //if (firstPacket.Length < 64)
        //{
        //    firstPacket.Dump();
        //    throw new ArgumentException($"Invalid first packet size:{firstPacket.Length}");
        //}

        if (firstPacket.Length < ConnectionPrefixBytes)
        {
            return ParseUnObfuscationFirstPacket(firstPacket);
        }

        // https://corefork.telegram.org/mtproto/mtproto-transports#transport-obfuscation
        var nonce = firstPacket[..ConnectionPrefixBytes];
        var sendKey = firstPacket.Slice(8, 32).ToArray();
        var sendIv = firstPacket.Slice(40, 16).ToArray();

        Span<byte> reversedBytes = stackalloc byte[48];
        firstPacket.Slice(8, 48).CopyTo(reversedBytes);
        reversedBytes.Reverse();

        var receiveKey = reversedBytes[..32].ToArray();
        var receiveIv = reversedBytes.Slice(32, 16).ToArray();

        var encryptedNonce = ArrayPool<byte>.Shared.Rent(nonce.Length);
        try
        {
            aesHelper.CtrEncrypt(nonce, encryptedNonce, sendKey, sendIv, 0);

            var data = new FirstPacketData
            {
                ObfuscationEnabled = true,
                ProtocolBufferLength = ConnectionPrefixBytes,
                SendCount = (uint)nonce.Length
            };

            var protocolBytes = encryptedNonce.AsSpan().Slice(56, 4);

            switch (protocolBytes)
            {
                case [AbridgedFlag, AbridgedFlag, AbridgedFlag, AbridgedFlag]:
                    data.ProtocolType = ProtocolType.Abridge;
                    break;
                case [IntermediateFlag, IntermediateFlag, IntermediateFlag, IntermediateFlag]:
                    data.ProtocolType = ProtocolType.Intermediate;
                    break;
                default:
                    logger.LogWarning("Unknown protocol: {nonce56:x} {nonce57:x} {nonce58:x} {nonce59:x}",
                        protocolBytes[0],
                        protocolBytes[1],
                        protocolBytes[2],
                        protocolBytes[3]);
                    break;
            }

            if (data.ProtocolType != ProtocolType.Unknown)
            {
                data.SendKey = sendKey;
                data.SendIv = sendIv;
                data.ReceiveKey = receiveKey;
                data.ReceiveIv = receiveIv;
            }

            return data;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(encryptedNonce);
        }
    }

    private FirstPacketData ParseUnObfuscationFirstPacket(ReadOnlySpan<byte> firstPacket)
    {
        var state = new FirstPacketData
        {
            ObfuscationEnabled = false,
            ProtocolBufferLength = 1
        };
        byte protocolByte = firstPacket[0];
        ProtocolType protocolType = ProtocolType.Unknown;
        switch (protocolByte)
        {
            case AbridgedFlag:
                protocolType = ProtocolType.Abridge;
                break;
            case IntermediateFlag:
                protocolType = ProtocolType.Intermediate;
                break;
            default:
                logger.LogWarning("UnKnown protocol: {Protocol}", firstPacket[0]);
                break;
        }

        if (firstPacket.Length == 4)
        {
            state.ProtocolBufferLength = 4;
        }

        state.ProtocolType = protocolType;

        logger.LogInformation("[{ProtocolType}](UnObfuscation) detected", state.ProtocolType);

        return state;
    }
}
