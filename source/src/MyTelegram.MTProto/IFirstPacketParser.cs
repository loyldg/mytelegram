namespace MyTelegram.MTProto;

public interface IFirstPacketParser
{
    FirstPacketData Parse(ReadOnlySpan<byte> firstPacket);
}
