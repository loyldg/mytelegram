namespace MyTelegram.MTProto;

public interface IClientData
{
    public long AuthKeyId { get; set; }
    public string ConnectionId { get; set; }
    //public ConnectionContext ConnectionContext { get; set; } = null!;
    public int CurrentPacketLength { get; set; }

    public bool IsFirstPacketParsed { get; set; }
    public ProtocolType MtProtoType { get; set; }
    public bool ObfuscationEnabled { get; set; }
    public CtrState ReceiveCtrState { get; set; }
    public byte[] ReceiveKey { get; set; }
    public CtrState SendCtrState { get; set; }
    public byte[] SendKey { get; set; }
    public int SkipCount { get; set; }
}

public interface IClientData<T> : IClientData
{
    T Data { get; set; }
}
