// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object describes message filter.
/// See <a href="https://corefork.telegram.org/constructor/MessagesFilter" />
///</summary>
[JsonDerivedType(typeof(TInputMessagesFilterEmpty), nameof(TInputMessagesFilterEmpty))]
[JsonDerivedType(typeof(TInputMessagesFilterPhotos), nameof(TInputMessagesFilterPhotos))]
[JsonDerivedType(typeof(TInputMessagesFilterVideo), nameof(TInputMessagesFilterVideo))]
[JsonDerivedType(typeof(TInputMessagesFilterPhotoVideo), nameof(TInputMessagesFilterPhotoVideo))]
[JsonDerivedType(typeof(TInputMessagesFilterDocument), nameof(TInputMessagesFilterDocument))]
[JsonDerivedType(typeof(TInputMessagesFilterUrl), nameof(TInputMessagesFilterUrl))]
[JsonDerivedType(typeof(TInputMessagesFilterGif), nameof(TInputMessagesFilterGif))]
[JsonDerivedType(typeof(TInputMessagesFilterVoice), nameof(TInputMessagesFilterVoice))]
[JsonDerivedType(typeof(TInputMessagesFilterMusic), nameof(TInputMessagesFilterMusic))]
[JsonDerivedType(typeof(TInputMessagesFilterChatPhotos), nameof(TInputMessagesFilterChatPhotos))]
[JsonDerivedType(typeof(TInputMessagesFilterPhoneCalls), nameof(TInputMessagesFilterPhoneCalls))]
[JsonDerivedType(typeof(TInputMessagesFilterRoundVoice), nameof(TInputMessagesFilterRoundVoice))]
[JsonDerivedType(typeof(TInputMessagesFilterRoundVideo), nameof(TInputMessagesFilterRoundVideo))]
[JsonDerivedType(typeof(TInputMessagesFilterMyMentions), nameof(TInputMessagesFilterMyMentions))]
[JsonDerivedType(typeof(TInputMessagesFilterGeo), nameof(TInputMessagesFilterGeo))]
[JsonDerivedType(typeof(TInputMessagesFilterContacts), nameof(TInputMessagesFilterContacts))]
[JsonDerivedType(typeof(TInputMessagesFilterPinned), nameof(TInputMessagesFilterPinned))]
public interface IMessagesFilter : IObject
{

}
