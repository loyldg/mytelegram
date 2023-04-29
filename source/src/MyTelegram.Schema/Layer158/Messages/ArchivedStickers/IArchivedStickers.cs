// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IArchivedStickers : IObject
{
    int Count { get; set; }
    TVector<Schema.IStickerSetCovered> Sets { get; set; }
}
