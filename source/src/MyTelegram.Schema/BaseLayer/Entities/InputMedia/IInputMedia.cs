// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Defines media content of a message.
/// See <a href="https://corefork.telegram.org/constructor/InputMedia" />
///</summary>
[JsonDerivedType(typeof(TInputMediaEmpty), nameof(TInputMediaEmpty))]
[JsonDerivedType(typeof(TInputMediaUploadedPhoto), nameof(TInputMediaUploadedPhoto))]
[JsonDerivedType(typeof(TInputMediaPhoto), nameof(TInputMediaPhoto))]
[JsonDerivedType(typeof(TInputMediaGeoPoint), nameof(TInputMediaGeoPoint))]
[JsonDerivedType(typeof(TInputMediaContact), nameof(TInputMediaContact))]
[JsonDerivedType(typeof(TInputMediaUploadedDocument), nameof(TInputMediaUploadedDocument))]
[JsonDerivedType(typeof(TInputMediaDocument), nameof(TInputMediaDocument))]
[JsonDerivedType(typeof(TInputMediaVenue), nameof(TInputMediaVenue))]
[JsonDerivedType(typeof(TInputMediaPhotoExternal), nameof(TInputMediaPhotoExternal))]
[JsonDerivedType(typeof(TInputMediaDocumentExternal), nameof(TInputMediaDocumentExternal))]
[JsonDerivedType(typeof(TInputMediaGame), nameof(TInputMediaGame))]
[JsonDerivedType(typeof(TInputMediaInvoice), nameof(TInputMediaInvoice))]
[JsonDerivedType(typeof(TInputMediaGeoLive), nameof(TInputMediaGeoLive))]
[JsonDerivedType(typeof(TInputMediaPoll), nameof(TInputMediaPoll))]
[JsonDerivedType(typeof(TInputMediaDice), nameof(TInputMediaDice))]
[JsonDerivedType(typeof(TInputMediaStory), nameof(TInputMediaStory))]
[JsonDerivedType(typeof(TInputMediaWebPage), nameof(TInputMediaWebPage))]
public interface IInputMedia : IObject
{

}
