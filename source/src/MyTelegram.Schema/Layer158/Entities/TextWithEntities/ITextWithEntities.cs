// ReSharper disable All

namespace MyTelegram.Schema;

public interface ITextWithEntities : IObject
{
    string Text { get; set; }
    TVector<Schema.IMessageEntity> Entities { get; set; }
}
