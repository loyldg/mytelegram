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
    public int DcId { get; set; }
    public ConnectionType ConnectionType { get; set; }
    public ProtocolType MtProtoType { get; set; }
    public bool IsFirstPacketParsed { get; set; }
    public byte[] SendKey { get; set; } = [];
    public byte[] ReceiveKey { get; set; } = [];
    public long AuthKeyId { get; set; }
    public string ConnectionId { get; set; } = null!;
    public bool ObfuscationEnabled { get; set; }
    public int CurrentPacketLength { get; set; }
    public int SkipCount { get; set; }
    public byte[] SendIv { get; set; } = [];
    public byte[] ReceiveIv { get; set; } = [];
    public ulong ReceiveCount { get; set; }
    public ulong SendCount { get; set; }
}