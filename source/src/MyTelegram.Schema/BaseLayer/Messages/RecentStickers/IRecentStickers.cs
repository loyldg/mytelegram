// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Recent stickers
/// See <a href="https://corefork.telegram.org/constructor/messages.RecentStickers" />
///</summary>
[JsonDerivedType(typeof(TRecentStickersNotModified), nameof(TRecentStickersNotModified))]
[JsonDerivedType(typeof(TRecentStickers), nameof(TRecentStickers))]
public interface IRecentStickers : IObject
{

}
