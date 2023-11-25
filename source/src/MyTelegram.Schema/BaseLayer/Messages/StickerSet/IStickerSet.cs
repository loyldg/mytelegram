// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Stickerset
/// See <a href="https://corefork.telegram.org/constructor/messages.StickerSet" />
///</summary>
[JsonDerivedType(typeof(TStickerSet), nameof(TStickerSet))]
[JsonDerivedType(typeof(TStickerSetNotModified), nameof(TStickerSetNotModified))]
public interface IStickerSet : IObject
{

}
