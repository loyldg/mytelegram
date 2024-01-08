// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Coordinates and size of a clicable rectangular area on top of a story.
/// See <a href="https://corefork.telegram.org/constructor/MediaAreaCoordinates" />
///</summary>
[JsonDerivedType(typeof(TMediaAreaCoordinates), nameof(TMediaAreaCoordinates))]
public interface IMediaAreaCoordinates : IObject
{
    ///<summary>
    /// The abscissa of the rectangle's center, as a percentage of the media width (0-100).
    ///</summary>
    double X { get; set; }

    ///<summary>
    /// The ordinate of the rectangle's center, as a percentage of the media height (0-100).
    ///</summary>
    double Y { get; set; }

    ///<summary>
    /// The width of the rectangle, as a percentage of the media width (0-100).
    ///</summary>
    double W { get; set; }

    ///<summary>
    /// The height of the rectangle, as a percentage of the media height (0-100).
    ///</summary>
    double H { get; set; }

    ///<summary>
    /// Clockwise rotation angle of the rectangle, in degrees (0-360).
    ///</summary>
    double Rotation { get; set; }
}
