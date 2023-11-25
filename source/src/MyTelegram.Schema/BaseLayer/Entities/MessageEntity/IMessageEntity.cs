// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Message entities, representing styled text in a message
/// See <a href="https://corefork.telegram.org/constructor/MessageEntity" />
///</summary>
[JsonDerivedType(typeof(TMessageEntityUnknown), nameof(TMessageEntityUnknown))]
[JsonDerivedType(typeof(TMessageEntityMention), nameof(TMessageEntityMention))]
[JsonDerivedType(typeof(TMessageEntityHashtag), nameof(TMessageEntityHashtag))]
[JsonDerivedType(typeof(TMessageEntityBotCommand), nameof(TMessageEntityBotCommand))]
[JsonDerivedType(typeof(TMessageEntityUrl), nameof(TMessageEntityUrl))]
[JsonDerivedType(typeof(TMessageEntityEmail), nameof(TMessageEntityEmail))]
[JsonDerivedType(typeof(TMessageEntityBold), nameof(TMessageEntityBold))]
[JsonDerivedType(typeof(TMessageEntityItalic), nameof(TMessageEntityItalic))]
[JsonDerivedType(typeof(TMessageEntityCode), nameof(TMessageEntityCode))]
[JsonDerivedType(typeof(TMessageEntityPre), nameof(TMessageEntityPre))]
[JsonDerivedType(typeof(TMessageEntityTextUrl), nameof(TMessageEntityTextUrl))]
[JsonDerivedType(typeof(TMessageEntityMentionName), nameof(TMessageEntityMentionName))]
[JsonDerivedType(typeof(TInputMessageEntityMentionName), nameof(TInputMessageEntityMentionName))]
[JsonDerivedType(typeof(TMessageEntityPhone), nameof(TMessageEntityPhone))]
[JsonDerivedType(typeof(TMessageEntityCashtag), nameof(TMessageEntityCashtag))]
[JsonDerivedType(typeof(TMessageEntityUnderline), nameof(TMessageEntityUnderline))]
[JsonDerivedType(typeof(TMessageEntityStrike), nameof(TMessageEntityStrike))]
[JsonDerivedType(typeof(TMessageEntityBankCard), nameof(TMessageEntityBankCard))]
[JsonDerivedType(typeof(TMessageEntitySpoiler), nameof(TMessageEntitySpoiler))]
[JsonDerivedType(typeof(TMessageEntityCustomEmoji), nameof(TMessageEntityCustomEmoji))]
[JsonDerivedType(typeof(TMessageEntityBlockquote), nameof(TMessageEntityBlockquote))]
public interface IMessageEntity : IObject
{
    ///<summary>
    /// Offset of message entity within message (in <a href="https://corefork.telegram.org/api/entities#entity-length">UTF-16 code units</a>)
    ///</summary>
    int Offset { get; set; }

    ///<summary>
    /// Length of message entity within message (in <a href="https://corefork.telegram.org/api/entities#entity-length">UTF-16 code units</a>)
    ///</summary>
    int Length { get; set; }
}
