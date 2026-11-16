namespace MyTelegram.MTProto;

public class FirstPacketData
{
    public bool ObfuscationEnabled { get; set; }
    public ProtocolType ProtocolType { get; set; }
    public byte[] ReceiveKey { get; set; } = [];
    public byte[] ReceiveIv { get; set; } = [];
    public byte[] SendKey { get; set; } = [];
    public byte[] SendIv { get; set; } = [];

    /// <summary>
    /// Number of bytes of the connection prefix that must be consumed (0xef, 0xeeeeeeee, 0xdddddddd,
    /// the 64-byte obfuscation header, or 0 for the full transport, which has no prefix).
    /// </summary>
    public int ProtocolBufferLength { get; set; }

    public ulong SendCount { get; set; }

    /// <summary>
    /// True when the received bytes are not enough to detect the transport yet.
    /// </summary>
    public bool NeedMoreData { get; set; }
}
