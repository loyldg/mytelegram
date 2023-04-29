// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface ITranslatedText : IObject
{
    TVector<Schema.ITextWithEntities> Result { get; set; }
}
