namespace MyTelegram.GatewayServer.Services;

public class ClientData : IClientData
{
    public ConnectionContext? ConnectionContext { get; set; }
    public WebSocket? WebSocket { get; set; }
    public ClientType ClientType { get; set; }
    public string ClientIp { get; set; } = null!;
    public long Seq { get; set; }

    public Channel<EncryptedMessageResponse> EncryptedMessageResponseQueue { get; set; } =
        Channel.CreateUnbounded<EncryptedMessageResponse>();

    public Channel<UnencryptedMessageResponse> UnencryptedMessageResponseQueue { get; set; } =
        Channel.CreateUnbounded<UnencryptedMessageResponse>();

    public Channel<UnencryptedMessage> UnencryptedMessageQueue { get; set; } =
        Channel.CreateUnbounded<UnencryptedMessage>();

    /// <summary>
    /// Quick ACK tokens to send to the client (already validated by <see cref="MtpTransportState.QuickAck"/>).
    /// </summary>
    public Channel<uint> QuickAckQueue { get; set; } =
        Channel.CreateUnbounded<uint>();

    ///// <summary>
    ///// Serializes "encode + write" of all outgoing frames of this connection.
    ///// Encoding advances the obfuscation counter and the full transport seqno, so frames must reach the
    ///// socket in the order they were encoded and must never interleave.
    ///// </summary>
    //public SemaphoreSlim SendLock { get; } = new(1, 1);

    public MtpTransportState TransportState { get; } = new();
    public int DcId { get; set; }
    public ConnectionType ConnectionType { get; set; }
    public ProtocolType MtProtoType { get; set; }
    public bool IsFirstPacketParsed { get; set; }
    public byte[] SendKey { get; set; } = [];
    public byte[] ReceiveKey { get; set; } = [];
    public long AuthKeyId { get; set; }
    public string ConnectionId { get; set; } = null!;
    public bool ObfuscationEnabled { get; set; }
    public byte[] SendIv { get; set; } = [];
    public byte[] ReceiveIv { get; set; } = [];
    public ulong ReceiveCount { get; set; }
    public ulong SendCount { get; set; }
}