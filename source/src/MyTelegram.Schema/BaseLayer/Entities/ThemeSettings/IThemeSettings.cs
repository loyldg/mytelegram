// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Theme settings
/// See <a href="https://corefork.telegram.org/constructor/ThemeSettings" />
///</summary>
[JsonDerivedType(typeof(TThemeSettings), nameof(TThemeSettings))]
public interface IThemeSettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, the freeform gradient fill needs to be animated on every sent message.
    ///</summary>
    bool MessageColorsAnimated { get; set; }

    ///<summary>
    /// Base theme
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
    /// <a href="https://corefork.telegram.org/api/wallpapers">Wallpaper</a>
    /// See <a href="https://corefork.telegram.org/type/WallPaper" />
    ///</summary>
    MyTelegram.Schema.IWallPaper? Wallpaper { get; set; }
}
