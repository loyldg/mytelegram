// ReSharper disable All

namespace MyTelegram.Schema;

public interface IWallPaperSettings : IObject
{
    BitArray Flags { get; set; }
    bool Blur { get; set; }
    bool Motion { get; set; }
    int? BackgroundColor { get; set; }
    int? SecondBackgroundColor { get; set; }
    int? ThirdBackgroundColor { get; set; }
    int? FourthBackgroundColor { get; set; }
    int? Intensity { get; set; }
    int? Rotation { get; set; }

}
