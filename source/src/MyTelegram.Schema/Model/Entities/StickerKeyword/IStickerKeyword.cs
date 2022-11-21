// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStickerKeyword : IObject
{
    long DocumentId { get; set; }
    TVector<string> Keyword { get; set; }

}
