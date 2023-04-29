// ReSharper disable All

namespace MyTelegram.Schema;

public interface IWebViewMessageSent : IObject
{
    BitArray Flags { get; set; }
    Schema.IInputBotInlineMessageID? MsgId { get; set; }
}
