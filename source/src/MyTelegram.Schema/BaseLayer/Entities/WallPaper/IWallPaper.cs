// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains info on a <a href="https://corefork.telegram.org/api/wallpapers">wallpaper</a>.
/// See <a href="https://corefork.telegram.org/constructor/WallPaper" />
///</summary>
public interface IWallPaper : IObject
{
    ///<summary>
    /// Wallpaper ID
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this is the default wallpaper
    ///</summary>
    bool Default { get; set; }

    ///<summary>
    /// Whether this wallpaper should be used in dark mode.
    ///</summary>
    bool Dark { get; set; }

    ///<summary>
    /// Info on how to generate the wallpaper.
    /// See <a href="https://corefork.telegram.org/type/WallPaperSettings" />
    ///</summary>
    MyTelegram.Schema.IWallPaperSettings? Settings { get; set; }
}
