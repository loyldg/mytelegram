// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Favorited stickers
/// See <a href="https://corefork.telegram.org/constructor/messages.FavedStickers" />
///</summary>
[JsonDerivedType(typeof(TFavedStickersNotModified), nameof(TFavedStickersNotModified))]
[JsonDerivedType(typeof(TFavedStickers), nameof(TFavedStickers))]
public interface IFavedStickers : IObject
{

}
