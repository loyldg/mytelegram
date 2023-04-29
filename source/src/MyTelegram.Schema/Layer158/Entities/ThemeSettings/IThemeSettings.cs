// ReSharper disable All

namespace MyTelegram.Schema;

public interface IThemeSettings : IObject
{
    BitArray Flags { get; set; }
    bool MessageColorsAnimated { get; set; }
    Schema.IBaseTheme BaseTheme { get; set; }
    int AccentColor { get; set; }
    int? OutboxAccentColor { get; set; }
    TVector<int>? MessageColors { get; set; }
    Schema.IWallPaper? Wallpaper { get; set; }
}
