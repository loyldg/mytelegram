// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Stickerset preview
/// See <a href="https://corefork.telegram.org/constructor/StickerSetCovered" />
///</summary>
[JsonDerivedType(typeof(TStickerSetCovered), nameof(TStickerSetCovered))]
[JsonDerivedType(typeof(TStickerSetMultiCovered), nameof(TStickerSetMultiCovered))]
[JsonDerivedType(typeof(TStickerSetFullCovered), nameof(TStickerSetFullCovered))]
[JsonDerivedType(typeof(TStickerSetNoCovered), nameof(TStickerSetNoCovered))]
public interface IStickerSetCovered : IObject
{
    ///<summary>
    /// Stickerset information.
    /// See <a href="https://corefork.telegram.org/type/StickerSet" />
    ///</summary>
    MyTelegram.Schema.IStickerSet Set { get; set; }
}
