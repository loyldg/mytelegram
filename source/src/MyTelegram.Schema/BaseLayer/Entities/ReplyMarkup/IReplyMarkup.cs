// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Reply markup for bot and inline keyboards
/// See <a href="https://corefork.telegram.org/constructor/ReplyMarkup" />
///</summary>
[JsonDerivedType(typeof(TReplyKeyboardHide), nameof(TReplyKeyboardHide))]
[JsonDerivedType(typeof(TReplyKeyboardForceReply), nameof(TReplyKeyboardForceReply))]
[JsonDerivedType(typeof(TReplyKeyboardMarkup), nameof(TReplyKeyboardMarkup))]
[JsonDerivedType(typeof(TReplyInlineMarkup), nameof(TReplyInlineMarkup))]
public interface IReplyMarkup : IObject
{

}
