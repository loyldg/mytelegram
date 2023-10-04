// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/MediaArea" />
///</summary>
public interface IMediaArea : IObject
{
    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/MediaAreaCoordinates" />
    ///</summary>
    MyTelegram.Schema.IMediaAreaCoordinates Coordinates { get; set; }
}
