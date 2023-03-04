// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStickerPack : IObject
{
    string Emoticon { get; set; }
    TVector<long> Documents { get; set; }
}
