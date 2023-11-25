// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// User actions. Use this to provide users with detailed info about their chat partner's actions: typing or sending attachments of all kinds.
/// See <a href="https://corefork.telegram.org/constructor/SendMessageAction" />
///</summary>
[JsonDerivedType(typeof(TSendMessageTypingAction), nameof(TSendMessageTypingAction))]
[JsonDerivedType(typeof(TSendMessageCancelAction), nameof(TSendMessageCancelAction))]
[JsonDerivedType(typeof(TSendMessageRecordVideoAction), nameof(TSendMessageRecordVideoAction))]
[JsonDerivedType(typeof(TSendMessageUploadVideoAction), nameof(TSendMessageUploadVideoAction))]
[JsonDerivedType(typeof(TSendMessageRecordAudioAction), nameof(TSendMessageRecordAudioAction))]
[JsonDerivedType(typeof(TSendMessageUploadAudioAction), nameof(TSendMessageUploadAudioAction))]
[JsonDerivedType(typeof(TSendMessageUploadPhotoAction), nameof(TSendMessageUploadPhotoAction))]
[JsonDerivedType(typeof(TSendMessageUploadDocumentAction), nameof(TSendMessageUploadDocumentAction))]
[JsonDerivedType(typeof(TSendMessageGeoLocationAction), nameof(TSendMessageGeoLocationAction))]
[JsonDerivedType(typeof(TSendMessageChooseContactAction), nameof(TSendMessageChooseContactAction))]
[JsonDerivedType(typeof(TSendMessageGamePlayAction), nameof(TSendMessageGamePlayAction))]
[JsonDerivedType(typeof(TSendMessageRecordRoundAction), nameof(TSendMessageRecordRoundAction))]
[JsonDerivedType(typeof(TSendMessageUploadRoundAction), nameof(TSendMessageUploadRoundAction))]
[JsonDerivedType(typeof(TSpeakingInGroupCallAction), nameof(TSpeakingInGroupCallAction))]
[JsonDerivedType(typeof(TSendMessageHistoryImportAction), nameof(TSendMessageHistoryImportAction))]
[JsonDerivedType(typeof(TSendMessageChooseStickerAction), nameof(TSendMessageChooseStickerAction))]
[JsonDerivedType(typeof(TSendMessageEmojiInteraction), nameof(TSendMessageEmojiInteraction))]
[JsonDerivedType(typeof(TSendMessageEmojiInteractionSeen), nameof(TSendMessageEmojiInteractionSeen))]
public interface ISendMessageAction : IObject
{

}
