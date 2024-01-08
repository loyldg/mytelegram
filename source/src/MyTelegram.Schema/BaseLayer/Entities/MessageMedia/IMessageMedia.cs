// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Media
/// See <a href="https://corefork.telegram.org/constructor/MessageMedia" />
///</summary>
[JsonDerivedType(typeof(TMessageMediaEmpty), nameof(TMessageMediaEmpty))]
[JsonDerivedType(typeof(TMessageMediaPhoto), nameof(TMessageMediaPhoto))]
[JsonDerivedType(typeof(TMessageMediaGeo), nameof(TMessageMediaGeo))]
[JsonDerivedType(typeof(TMessageMediaContact), nameof(TMessageMediaContact))]
[JsonDerivedType(typeof(TMessageMediaUnsupported), nameof(TMessageMediaUnsupported))]
[JsonDerivedType(typeof(TMessageMediaDocument), nameof(TMessageMediaDocument))]
[JsonDerivedType(typeof(TMessageMediaWebPage), nameof(TMessageMediaWebPage))]
[JsonDerivedType(typeof(TMessageMediaVenue), nameof(TMessageMediaVenue))]
[JsonDerivedType(typeof(TMessageMediaGame), nameof(TMessageMediaGame))]
[JsonDerivedType(typeof(TMessageMediaInvoice), nameof(TMessageMediaInvoice))]
[JsonDerivedType(typeof(TMessageMediaGeoLive), nameof(TMessageMediaGeoLive))]
[JsonDerivedType(typeof(TMessageMediaPoll), nameof(TMessageMediaPoll))]
[JsonDerivedType(typeof(TMessageMediaDice), nameof(TMessageMediaDice))]
[JsonDerivedType(typeof(TMessageMediaStory), nameof(TMessageMediaStory))]
[JsonDerivedType(typeof(TMessageMediaGiveaway), nameof(TMessageMediaGiveaway))]
[JsonDerivedType(typeof(TMessageMediaGiveawayResults), nameof(TMessageMediaGiveawayResults))]
public interface IMessageMedia : IObject
{

}
