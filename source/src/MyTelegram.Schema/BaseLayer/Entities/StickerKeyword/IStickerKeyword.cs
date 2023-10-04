// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Keywords for a certain sticker
/// See <a href="https://corefork.telegram.org/constructor/StickerKeyword" />
///</summary>
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
