namespace MyTelegram.MTProto;

/// <summary>
/// Detects the MTProto transport from the first bytes of a TCP/WebSocket connection.
/// https://core.telegram.org/mtproto/mtproto-transports
/// </summary>
public class FirstPacketParser(
    ILogger<FirstPacketParser> logger,
    IAesHelper aesHelper)
    : IFirstPacketParser, ITransientDependency
{
    public const int ConnectionPrefixBytes = 64;

    private const byte AbridgedFlag = 0xef;
    private const uint AbridgedTag = 0xefefefef;
    private const uint IntermediateTag = 0xeeeeeeee;
    private const uint PaddedIntermediateTag = 0xdddddddd;

    // Identifiers that tdlib refuses to generate as the first int of an obfuscation header,
    // because the server uses them to recognize other protocols (HTTP methods and TLS).
    private const uint HttpHead = 0x44414548; // "HEAD"
    private const uint HttpPost = 0x54534f50; // "POST"
    private const uint HttpGet = 0x20544547; // "GET"
    private const uint HttpOptions = 0x4954504f; // "OPTI"
    private const uint TlsClientHello = 0x02010316;

    /// <summary>
    /// Parses the connection prefix.
    /// When the bytes are not enough to make a decision, <see cref="FirstPacketData.NeedMoreData"/> is set
    /// and nothing must be consumed; the caller retries with more data.
    /// </summary>
    public FirstPacketData Parse(ReadOnlySpan<byte> firstPacket)
    {
        // Detection order (all checks are unambiguous because a valid obfuscation header never starts
        // with 0xef, 0xeeeeeeee or 0xdddddddd and never has zero in bytes 4-8):
        // 1. 0xef                    -> abridged
        // 2. 0xeeeeeeee              -> intermediate
        // 3. 0xdddddddd              -> padded intermediate
        // 4. bytes 4-8 == 0x00000000 -> full (the first frame starts immediately: len + seqno 0)
        // 5. otherwise               -> 64-byte obfuscation header, protocol tag at offset 56
        if (firstPacket.IsEmpty)
        {
            return NeedMoreData();
        }

        if (firstPacket[0] == AbridgedFlag)
        {
            return CreateUnobfuscated(ProtocolType.Abridge, 1);
        }

        if (firstPacket.Length < 4)
        {
            return NeedMoreData();
        }

        var firstInt = BinaryPrimitives.ReadUInt32LittleEndian(firstPacket);
        switch (firstInt)
        {
            case IntermediateTag:
                return CreateUnobfuscated(ProtocolType.Intermediate, 4);
            case PaddedIntermediateTag:
                return CreateUnobfuscated(ProtocolType.PaddedIntermediate, 4);
            case HttpHead or HttpPost or HttpGet or HttpOptions or TlsClientHello:
                logger.LogWarning("Unsupported protocol on MTProto TCP port, first int: {FirstInt:x8}", firstInt);
                return new FirstPacketData { ProtocolType = ProtocolType.Unknown };
        }

        if (firstPacket.Length < 8)
        {
            return NeedMoreData();
        }

        if (BinaryPrimitives.ReadUInt32LittleEndian(firstPacket[4..]) == 0)
        {
            // Full transport: there is no prefix, the bytes belong to the first frame (len, seqno = 0).
            return CreateUnobfuscated(ProtocolType.Full, 0);
        }

        if (firstPacket.Length < ConnectionPrefixBytes)
        {
            return NeedMoreData();
        }

        return ParseObfuscated(firstPacket[..ConnectionPrefixBytes]);
    }

    private FirstPacketData ParseObfuscated(ReadOnlySpan<byte> nonce)
    {
        // https://core.telegram.org/mtproto/mtproto-transports#transport-obfuscation
        // The client encrypts with key = init[8..40], iv = init[40..56];
        // it decrypts with the same ranges taken from the reversed init payload.
        var sendKey = nonce.Slice(8, 32).ToArray();
        var sendIv = nonce.Slice(40, 16).ToArray();

        Span<byte> reversedBytes = stackalloc byte[48];
        nonce.Slice(8, 48).CopyTo(reversedBytes);
        reversedBytes.Reverse();

        var receiveKey = reversedBytes[..32].ToArray();
        var receiveIv = reversedBytes.Slice(32, 16).ToArray();

        Span<byte> decryptedNonce = stackalloc byte[ConnectionPrefixBytes];
        aesHelper.CtrEncrypt(nonce, decryptedNonce, sendKey, sendIv, 0);

        var data = new FirstPacketData
        {
            ObfuscationEnabled = true,
            ProtocolBufferLength = ConnectionPrefixBytes,
            SendCount = ConnectionPrefixBytes
        };

        var protocolTag = BinaryPrimitives.ReadUInt32LittleEndian(decryptedNonce.Slice(56, 4));
        data.ProtocolType = protocolTag switch
        {
            AbridgedTag => ProtocolType.Abridge,
            IntermediateTag => ProtocolType.Intermediate,
            PaddedIntermediateTag => ProtocolType.PaddedIntermediate,
            // The documentation defines no protocol tag for the full transport inside obfuscation,
            // so it is not accepted here.
            _ => ProtocolType.Unknown
        };

        if (data.ProtocolType == ProtocolType.Unknown)
        {
            logger.LogWarning("Unknown obfuscated protocol tag: {ProtocolTag:x8}", protocolTag);
            return data;
        }

        data.SendKey = sendKey;
        data.SendIv = sendIv;
        data.ReceiveKey = receiveKey;
        data.ReceiveIv = receiveIv;

        logger.LogDebug("[{ProtocolType}](Obfuscated) detected, dc id field: {DcId}",
            data.ProtocolType,
            BinaryPrimitives.ReadInt16LittleEndian(decryptedNonce.Slice(60, 2)));

        return data;
    }

    private FirstPacketData CreateUnobfuscated(ProtocolType protocolType, int prefixLength)
    {
        logger.LogDebug("[{ProtocolType}](UnObfuscation) detected", protocolType);

        return new FirstPacketData
        {
            ObfuscationEnabled = false,
            ProtocolType = protocolType,
            ProtocolBufferLength = prefixLength
        };
    }

    private static FirstPacketData NeedMoreData()
    {
        return new FirstPacketData { NeedMoreData = true };
    }
}
