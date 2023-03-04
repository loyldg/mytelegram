// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessageInteractionCounters : IObject
{
    int MsgId { get; set; }
    int Views { get; set; }
    int Forwards { get; set; }
}
