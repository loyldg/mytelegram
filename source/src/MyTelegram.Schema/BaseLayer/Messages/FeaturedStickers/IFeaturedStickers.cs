// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Featured stickers
/// See <a href="https://corefork.telegram.org/constructor/messages.FeaturedStickers" />
///</summary>
public interface IFeaturedStickers : IObject
{
    ///<summary>
    /// Total number of featured stickers
    ///</summary>
    int Count { get; set; }
}
