// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/MediaAreaCoordinates" />
///</summary>
public interface IMediaAreaCoordinates : IObject
{
    ///<summary>
    /// &nbsp;
    ///</summary>
    double X { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    double Y { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    double W { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    double H { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    double Rotation { get; set; }
}
