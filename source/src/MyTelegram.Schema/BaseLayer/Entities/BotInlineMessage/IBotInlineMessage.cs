// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Inline message
/// See <a href="https://corefork.telegram.org/constructor/BotInlineMessage" />
///</summary>
[JsonDerivedType(typeof(TBotInlineMessageMediaAuto), nameof(TBotInlineMessageMediaAuto))]
[JsonDerivedType(typeof(TBotInlineMessageText), nameof(TBotInlineMessageText))]
[JsonDerivedType(typeof(TBotInlineMessageMediaGeo), nameof(TBotInlineMessageMediaGeo))]
[JsonDerivedType(typeof(TBotInlineMessageMediaVenue), nameof(TBotInlineMessageMediaVenue))]
[JsonDerivedType(typeof(TBotInlineMessageMediaContact), nameof(TBotInlineMessageMediaContact))]
[JsonDerivedType(typeof(TBotInlineMessageMediaInvoice), nameof(TBotInlineMessageMediaInvoice))]
[JsonDerivedType(typeof(TBotInlineMessageMediaWebPage), nameof(TBotInlineMessageMediaWebPage))]
public interface IBotInlineMessage : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IReplyMarkup? ReplyMarkup { get; set; }
}
