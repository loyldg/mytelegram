// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/api/stories#media-areas">story media area »</a>
/// See <a href="https://corefork.telegram.org/constructor/MediaArea" />
///</summary>
[JsonDerivedType(typeof(TMediaAreaVenue), nameof(TMediaAreaVenue))]
[JsonDerivedType(typeof(TInputMediaAreaVenue), nameof(TInputMediaAreaVenue))]
[JsonDerivedType(typeof(TMediaAreaGeoPoint), nameof(TMediaAreaGeoPoint))]
[JsonDerivedType(typeof(TMediaAreaSuggestedReaction), nameof(TMediaAreaSuggestedReaction))]
[JsonDerivedType(typeof(TMediaAreaChannelPost), nameof(TMediaAreaChannelPost))]
[JsonDerivedType(typeof(TInputMediaAreaChannelPost), nameof(TInputMediaAreaChannelPost))]
public interface IMediaArea : IObject
{
    MyTelegram.Schema.IMediaAreaCoordinates Coordinates { get; set; }
}
