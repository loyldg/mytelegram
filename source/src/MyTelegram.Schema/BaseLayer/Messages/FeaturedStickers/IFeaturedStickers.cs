// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Featured stickers
/// See <a href="https://corefork.telegram.org/constructor/messages.FeaturedStickers" />
///</summary>
[JsonDerivedType(typeof(TFeaturedStickersNotModified), nameof(TFeaturedStickersNotModified))]
[JsonDerivedType(typeof(TFeaturedStickers), nameof(TFeaturedStickers))]
public interface IFeaturedStickers : IObject
{
    ///<summary>
    /// Total number of featured stickers
    ///</summary>
    int Count { get; set; }
}
