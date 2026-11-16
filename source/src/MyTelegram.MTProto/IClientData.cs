namespace MyTelegram.MTProto;

public interface IClientData
{
    public long AuthKeyId { get; set; }

    public string ConnectionId { get; set; }

    public bool IsFirstPacketParsed { get; set; }
    public ProtocolType MtProtoType { get; set; }
    public bool ObfuscationEnabled { get; set; }

    /// <summary>
    /// Obfuscation key used to encrypt data sent to the client (server -> client direction).
    /// </summary>
    public byte[] ReceiveKey { get; set; }

    /// <summary>
    /// Obfuscation key used to decrypt data sent by the client (client -> server direction).
    /// </summary>
    public byte[] SendKey { get; set; }

    public byte[] SendIv { get; set; }
    public byte[] ReceiveIv { get; set; }

    /// <summary>
    /// AES-CTR byte offset of the server -> client stream.
    /// </summary>
    public ulong ReceiveCount { get; set; }

    /// <summary>
    /// AES-CTR byte offset of the client -> server stream.
    /// </summary>
    public ulong SendCount { get; set; }

    /// <summary>
    /// Transport framing state (partial frame header, full transport sequence numbers, quick ACK requests).
    /// </summary>
    public MtpTransportState TransportState { get; }
}

public interface IClientData<T> : IClientData
{
    T Data { get; set; }
}
