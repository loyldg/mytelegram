// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Styled text with <a href="https://corefork.telegram.org/api/entities">message entities</a>
/// See <a href="https://corefork.telegram.org/constructor/TextWithEntities" />
///</summary>
[JsonDerivedType(typeof(TTextWithEntities), nameof(TTextWithEntities))]
public interface ITextWithEntities : IObject
{
    ///<summary>
    /// Text
    ///</summary>
    string Text { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/entities">Message entities for styled text</a>
    /// See <a href="https://corefork.telegram.org/type/MessageEntity" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageEntity> Entities { get; set; }
}
