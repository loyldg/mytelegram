namespace MyTelegram.GatewayServer.Services;

public class ClientData : IClientData
{
    public ConnectionContext? ConnectionContext { get; set; }
    public WebSocket? WebSocket { get; set; }
    public ClientType ClientType { get; set; }
    public string ClientIp { get; set; } = null!;
    public ProtocolType MtProtoType { get; set; }
    public bool IsFirstPacketParsed { get; set; }
    public byte[] SendKey { get; set; } = null!;
    public byte[] ReceiveKey { get; set; } = null!;
    public CtrState SendCtrState { get; set; } = null!;
    public CtrState ReceiveCtrState { get; set; } = null!;
    public long AuthKeyId { get; set; }
    public string ConnectionId { get; set; } = null!;
    public bool ObfuscationEnabled { get; set; }
    public int CurrentPacketLength { get; set; }
    public int SkipCount { get; set; }
}
