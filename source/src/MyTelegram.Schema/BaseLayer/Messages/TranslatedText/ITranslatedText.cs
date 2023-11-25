// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Translated text with <a href="https://corefork.telegram.org/api/entities">entities</a>.
/// See <a href="https://corefork.telegram.org/constructor/messages.TranslatedText" />
///</summary>
[JsonDerivedType(typeof(TTranslateResult), nameof(TTranslateResult))]
public interface ITranslatedText : IObject
{
    ///<summary>
    /// Text+<a href="https://corefork.telegram.org/api/entities">entities</a>, for each input message.
    /// See <a href="https://corefork.telegram.org/type/TextWithEntities" />
    ///</summary>
    TVector<MyTelegram.Schema.ITextWithEntities> Result { get; set; }
}
