namespace MyTelegram.MTProto;

public class CtrState
{
    public byte[] ECounter { get; set; } = new byte[16];
    public byte[] Iv { get; set; } = new byte[16];
    public int Number { get; set; }
}
