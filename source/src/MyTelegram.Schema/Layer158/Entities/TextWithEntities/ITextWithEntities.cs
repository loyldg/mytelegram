// ReSharper disable All

namespace MyTelegram.Schema;

public interface ITextWithEntities : IObject
{
    string Text { get; set; }
    TVector<MyTelegram.Schema.IMessageEntity> Entities { get; set; }
}
