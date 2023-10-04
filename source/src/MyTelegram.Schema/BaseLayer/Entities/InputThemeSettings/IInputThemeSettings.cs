// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Theme settings
/// See <a href="https://corefork.telegram.org/constructor/InputThemeSettings" />
///</summary>
public interface IInputThemeSettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, the freeform gradient fill needs to be animated on every sent message
    ///</summary>
    bool MessageColorsAnimated { get; set; }

    ///<summary>
    /// Default theme on which this theme is based
    /// See <a href="https://corefork.telegram.org/type/BaseTheme" />
    ///</summary>
    MyTelegram.Schema.IBaseTheme BaseTheme { get; set; }

    ///<summary>
    /// Accent color, ARGB format
    ///</summary>
    int AccentColor { get; set; }

    ///<summary>
    /// Accent color of outgoing messages in ARGB format
    ///</summary>
    int? OutboxAccentColor { get; set; }

    ///<summary>
    /// The fill to be used as a background for outgoing messages, in RGB24 format. <br>If just one or two equal colors are provided, describes a solid fill of a background. <br>If two different colors are provided, describes the top and bottom colors of a 0-degree gradient.<br>If three or four colors are provided, describes a freeform gradient fill of a background.
    ///</summary>
    TVector<int>? MessageColors { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/constructor/inputWallPaper">inputWallPaper</a> or <a href="https://corefork.telegram.org/constructor/inputWallPaper">inputWallPaperSlug</a> when passing wallpaper files for <a href="https://corefork.telegram.org/api/wallpapers#image-wallpapers">image</a> or <a href="https://corefork.telegram.org/api/wallpapers#pattern-wallpapers">pattern</a> wallpapers, <a href="https://corefork.telegram.org/constructor/inputWallPaperNoFile">inputWallPaperNoFile</a> with <code>id=0</code> otherwise.
    /// See <a href="https://corefork.telegram.org/type/InputWallPaper" />
    ///</summary>
    MyTelegram.Schema.IInputWallPaper? Wallpaper { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/wallpapers">Wallpaper</a> settings.
    /// See <a href="https://corefork.telegram.org/type/WallPaperSettings" />
    ///</summary>
    MyTelegram.Schema.IWallPaperSettings? WallpaperSettings { get; set; }
}
