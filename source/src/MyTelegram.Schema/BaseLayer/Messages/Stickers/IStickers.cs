// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Stickers
/// See <a href="https://corefork.telegram.org/constructor/messages.Stickers" />
///</summary>
[JsonDerivedType(typeof(TStickersNotModified), nameof(TStickersNotModified))]
[JsonDerivedType(typeof(TStickers), nameof(TStickers))]
public interface IStickers : IObject
{

}
