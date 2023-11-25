// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Keywords for a certain sticker
/// See <a href="https://corefork.telegram.org/constructor/StickerKeyword" />
///</summary>
[JsonDerivedType(typeof(TStickerKeyword), nameof(TStickerKeyword))]
public interface IStickerKeyword : IObject
{
    ///<summary>
    /// Sticker ID
    ///</summary>
    long DocumentId { get; set; }

    ///<summary>
    /// Keywords
    ///</summary>
    TVector<string> Keyword { get; set; }
}
