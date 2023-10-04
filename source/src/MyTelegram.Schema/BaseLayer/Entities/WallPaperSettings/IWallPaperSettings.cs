// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Wallpaper rendering information.
/// See <a href="https://corefork.telegram.org/constructor/WallPaperSettings" />
///</summary>
public interface IWallPaperSettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// For <a href="https://corefork.telegram.org/api/wallpapers#image-wallpapers">image wallpapers »</a>: if set, the JPEG must be downscaled to fit in 450x450 square and then box-blurred with radius 12.
    ///</summary>
    bool Blur { get; set; }

    ///<summary>
    /// If set, the background needs to be slightly moved when the device is rotated.
    ///</summary>
    bool Motion { get; set; }

    ///<summary>
    /// Used for <a href="https://corefork.telegram.org/api/wallpapers#solid-fill">solid »</a>, <a href="https://corefork.telegram.org/api/wallpapers#gradient-fill">gradient »</a> and <a href="https://corefork.telegram.org/api/wallpapers#freeform-gradient-fill">freeform gradient »</a> fills.
    ///</summary>
    int? BackgroundColor { get; set; }

    ///<summary>
    /// Used for <a href="https://corefork.telegram.org/api/wallpapers#gradient-fill">gradient »</a> and <a href="https://corefork.telegram.org/api/wallpapers#freeform-gradient-fill">freeform gradient »</a> fills.
    ///</summary>
    int? SecondBackgroundColor { get; set; }

    ///<summary>
    /// Used for <a href="https://corefork.telegram.org/api/wallpapers#freeform-gradient-fill">freeform gradient »</a> fills.
    ///</summary>
    int? ThirdBackgroundColor { get; set; }

    ///<summary>
    /// Used for <a href="https://corefork.telegram.org/api/wallpapers#freeform-gradient-fill">freeform gradient »</a> fills.
    ///</summary>
    int? FourthBackgroundColor { get; set; }

    ///<summary>
    /// Used for <a href="https://corefork.telegram.org/api/wallpapers#pattern-wallpapers">pattern wallpapers »</a>.
    ///</summary>
    int? Intensity { get; set; }

    ///<summary>
    /// Clockwise rotation angle of the gradient, in degrees; 0-359. Should be always divisible by 45.
    ///</summary>
    int? Rotation { get; set; }
}
