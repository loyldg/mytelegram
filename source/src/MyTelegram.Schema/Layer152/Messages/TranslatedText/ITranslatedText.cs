// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface ITranslatedText : IObject
{
    TVector<MyTelegram.Schema.ITextWithEntities> Result { get; set; }
}
