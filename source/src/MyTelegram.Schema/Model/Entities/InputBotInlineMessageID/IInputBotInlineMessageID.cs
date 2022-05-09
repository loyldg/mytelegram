// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputBotInlineMessageID : IObject
{
    int DcId { get; set; }
    long AccessHash { get; set; }

}
