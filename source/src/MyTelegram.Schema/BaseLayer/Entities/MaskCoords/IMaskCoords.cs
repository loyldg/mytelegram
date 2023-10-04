// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Mask coordinates (if this is a mask sticker, attached to a photo)
/// See <a href="https://corefork.telegram.org/constructor/MaskCoords" />
///</summary>
public interface IMaskCoords : IObject
{
    ///<summary>
    /// Part of the face, relative to which the mask should be placed
    ///</summary>
    int N { get; set; }

    ///<summary>
    /// Shift by X-axis measured in widths of the mask scaled to the face size, from left to right. (For example, -1.0 will place the mask just to the left of the default mask position)
    ///</summary>
    double X { get; set; }

    ///<summary>
    /// Shift by Y-axis measured in widths of the mask scaled to the face size, from left to right. (For example, -1.0 will place the mask just below the default mask position)
    ///</summary>
    double Y { get; set; }

    ///<summary>
    /// Mask scaling coefficient. (For example, 2.0 means a doubled size)
    ///</summary>
    double Zoom { get; set; }
}
