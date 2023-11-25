// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// All stickers
/// See <a href="https://corefork.telegram.org/constructor/messages.AllStickers" />
///</summary>
[JsonDerivedType(typeof(TAllStickersNotModified), nameof(TAllStickersNotModified))]
[JsonDerivedType(typeof(TAllStickers), nameof(TAllStickers))]
public interface IAllStickers : IObject
{

}
