// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputThemeSettings : IObject
{
    BitArray Flags { get; set; }
    bool MessageColorsAnimated { get; set; }
    Schema.IBaseTheme BaseTheme { get; set; }
    int AccentColor { get; set; }
    int? OutboxAccentColor { get; set; }
    TVector<int>? MessageColors { get; set; }
    Schema.IInputWallPaper? Wallpaper { get; set; }
    Schema.IWallPaperSettings? WallpaperSettings { get; set; }
}
