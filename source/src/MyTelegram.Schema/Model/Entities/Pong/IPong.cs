// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPong : IObject
{
    long MsgId { get; set; }
    long PingId { get; set; }

}
