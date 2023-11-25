// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/MediaArea" />
///</summary>
[JsonDerivedType(typeof(TMediaAreaVenue), nameof(TMediaAreaVenue))]
[JsonDerivedType(typeof(TInputMediaAreaVenue), nameof(TInputMediaAreaVenue))]
[JsonDerivedType(typeof(TMediaAreaGeoPoint), nameof(TMediaAreaGeoPoint))]
[JsonDerivedType(typeof(TMediaAreaSuggestedReaction), nameof(TMediaAreaSuggestedReaction))]
public interface IMediaArea : IObject
{
    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/MediaAreaCoordinates" />
    ///</summary>
    MyTelegram.Schema.IMediaAreaCoordinates Coordinates { get; set; }
}
