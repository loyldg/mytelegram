// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputThemeSettings : IObject
{
    BitArray Flags { get; set; }
    bool MessageColorsAnimated { get; set; }
    MyTelegram.Schema.IBaseTheme BaseTheme { get; set; }
    int AccentColor { get; set; }
    int? OutboxAccentColor { get; set; }
    TVector<int>? MessageColors { get; set; }
    MyTelegram.Schema.IInputWallPaper? Wallpaper { get; set; }
    MyTelegram.Schema.IWallPaperSettings? WallpaperSettings { get; set; }

}
