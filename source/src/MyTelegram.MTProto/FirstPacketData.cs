namespace MyTelegram.MTProto;

public class FirstPacketData
{
    public bool ObfuscationEnabled { get; set; }
    public ProtocolType ProtocolType { get; set; }
    public byte[] ReceiveKey { get; set; } = default!;
    public CtrState ReceiveState { get; set; } = default!;
    public byte[] SendKey { get; set; } = default!;
    public CtrState SendState { get; set; } = default!;
}
