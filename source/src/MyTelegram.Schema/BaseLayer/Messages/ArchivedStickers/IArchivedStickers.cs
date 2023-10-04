// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Archived stickers
/// See <a href="https://corefork.telegram.org/constructor/messages.ArchivedStickers" />
///</summary>
public interface IArchivedStickers : IObject
{
    ///<summary>
    /// Number of archived stickers
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// Archived stickersets
    /// See <a href="https://corefork.telegram.org/type/StickerSetCovered" />
    ///</summary>
    TVector<MyTelegram.Schema.IStickerSetCovered> Sets { get; set; }
}
