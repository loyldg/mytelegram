// ReSharper disable All

namespace MyTelegram.Schema.Stickers;

///<summary>
/// A suggested short name for the specified stickerpack
/// See <a href="https://corefork.telegram.org/constructor/stickers.SuggestedShortName" />
///</summary>
public interface ISuggestedShortName : IObject
{
    ///<summary>
    /// Suggested short name
    ///</summary>
    string ShortName { get; set; }
}
