// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Sticker
/// See <a href="https://corefork.telegram.org/constructor/InputStickerSetItem" />
///</summary>
public interface IInputStickerSetItem : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// The sticker
    /// See <a href="https://corefork.telegram.org/type/InputDocument" />
    ///</summary>
    MyTelegram.Schema.IInputDocument Document { get; set; }

    ///<summary>
    /// Associated emoji
    ///</summary>
    string Emoji { get; set; }

    ///<summary>
    /// Coordinates for mask sticker
    /// See <a href="https://corefork.telegram.org/type/MaskCoords" />
    ///</summary>
    MyTelegram.Schema.IMaskCoords? MaskCoords { get; set; }

    ///<summary>
    /// Set of keywords, separated by commas (can't be provided for mask stickers)
    ///</summary>
    string? Keywords { get; set; }
}
