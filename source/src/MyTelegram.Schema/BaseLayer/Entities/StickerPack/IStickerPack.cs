// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Stickerpack
/// See <a href="https://corefork.telegram.org/constructor/StickerPack" />
///</summary>
[JsonDerivedType(typeof(TStickerPack), nameof(TStickerPack))]
public interface IStickerPack : IObject
{
    ///<summary>
    /// Emoji
    ///</summary>
    string Emoticon { get; set; }

    ///<summary>
    /// Stickers
    ///</summary>
    TVector<long> Documents { get; set; }
}
