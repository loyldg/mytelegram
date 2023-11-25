// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a sent inline message from the perspective of a bot
/// See <a href="https://corefork.telegram.org/constructor/InputBotInlineMessage" />
///</summary>
[JsonDerivedType(typeof(TInputBotInlineMessageMediaAuto), nameof(TInputBotInlineMessageMediaAuto))]
[JsonDerivedType(typeof(TInputBotInlineMessageText), nameof(TInputBotInlineMessageText))]
[JsonDerivedType(typeof(TInputBotInlineMessageMediaGeo), nameof(TInputBotInlineMessageMediaGeo))]
[JsonDerivedType(typeof(TInputBotInlineMessageMediaVenue), nameof(TInputBotInlineMessageMediaVenue))]
[JsonDerivedType(typeof(TInputBotInlineMessageMediaContact), nameof(TInputBotInlineMessageMediaContact))]
[JsonDerivedType(typeof(TInputBotInlineMessageGame), nameof(TInputBotInlineMessageGame))]
[JsonDerivedType(typeof(TInputBotInlineMessageMediaInvoice), nameof(TInputBotInlineMessageMediaInvoice))]
[JsonDerivedType(typeof(TInputBotInlineMessageMediaWebPage), nameof(TInputBotInlineMessageMediaWebPage))]
public interface IInputBotInlineMessage : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IReplyMarkup? ReplyMarkup { get; set; }
}
