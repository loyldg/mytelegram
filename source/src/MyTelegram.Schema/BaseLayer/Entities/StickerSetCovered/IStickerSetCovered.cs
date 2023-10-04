// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Stickerset preview
/// See <a href="https://corefork.telegram.org/constructor/StickerSetCovered" />
///</summary>
public interface IStickerSetCovered : IObject
{
    ///<summary>
    /// Stickerset information.
    /// See <a href="https://corefork.telegram.org/type/StickerSet" />
    ///</summary>
    MyTelegram.Schema.IStickerSet Set { get; set; }
}
