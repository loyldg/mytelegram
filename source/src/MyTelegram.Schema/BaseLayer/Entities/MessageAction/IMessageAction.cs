// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object describing actions connected to a service message.
/// See <a href="https://corefork.telegram.org/constructor/MessageAction" />
///</summary>
[JsonDerivedType(typeof(TMessageActionEmpty), nameof(TMessageActionEmpty))]
[JsonDerivedType(typeof(TMessageActionChatCreate), nameof(TMessageActionChatCreate))]
[JsonDerivedType(typeof(TMessageActionChatEditTitle), nameof(TMessageActionChatEditTitle))]
[JsonDerivedType(typeof(TMessageActionChatEditPhoto), nameof(TMessageActionChatEditPhoto))]
[JsonDerivedType(typeof(TMessageActionChatDeletePhoto), nameof(TMessageActionChatDeletePhoto))]
[JsonDerivedType(typeof(TMessageActionChatAddUser), nameof(TMessageActionChatAddUser))]
[JsonDerivedType(typeof(TMessageActionChatDeleteUser), nameof(TMessageActionChatDeleteUser))]
[JsonDerivedType(typeof(TMessageActionChatJoinedByLink), nameof(TMessageActionChatJoinedByLink))]
[JsonDerivedType(typeof(TMessageActionChannelCreate), nameof(TMessageActionChannelCreate))]
[JsonDerivedType(typeof(TMessageActionChatMigrateTo), nameof(TMessageActionChatMigrateTo))]
[JsonDerivedType(typeof(TMessageActionChannelMigrateFrom), nameof(TMessageActionChannelMigrateFrom))]
[JsonDerivedType(typeof(TMessageActionPinMessage), nameof(TMessageActionPinMessage))]
[JsonDerivedType(typeof(TMessageActionHistoryClear), nameof(TMessageActionHistoryClear))]
[JsonDerivedType(typeof(TMessageActionGameScore), nameof(TMessageActionGameScore))]
[JsonDerivedType(typeof(TMessageActionPaymentSentMe), nameof(TMessageActionPaymentSentMe))]
[JsonDerivedType(typeof(TMessageActionPaymentSent), nameof(TMessageActionPaymentSent))]
[JsonDerivedType(typeof(TMessageActionPhoneCall), nameof(TMessageActionPhoneCall))]
[JsonDerivedType(typeof(TMessageActionScreenshotTaken), nameof(TMessageActionScreenshotTaken))]
[JsonDerivedType(typeof(TMessageActionCustomAction), nameof(TMessageActionCustomAction))]
[JsonDerivedType(typeof(TMessageActionBotAllowed), nameof(TMessageActionBotAllowed))]
[JsonDerivedType(typeof(TMessageActionSecureValuesSentMe), nameof(TMessageActionSecureValuesSentMe))]
[JsonDerivedType(typeof(TMessageActionSecureValuesSent), nameof(TMessageActionSecureValuesSent))]
[JsonDerivedType(typeof(TMessageActionContactSignUp), nameof(TMessageActionContactSignUp))]
[JsonDerivedType(typeof(TMessageActionGeoProximityReached), nameof(TMessageActionGeoProximityReached))]
[JsonDerivedType(typeof(TMessageActionGroupCall), nameof(TMessageActionGroupCall))]
[JsonDerivedType(typeof(TMessageActionInviteToGroupCall), nameof(TMessageActionInviteToGroupCall))]
[JsonDerivedType(typeof(TMessageActionSetMessagesTTL), nameof(TMessageActionSetMessagesTTL))]
[JsonDerivedType(typeof(TMessageActionGroupCallScheduled), nameof(TMessageActionGroupCallScheduled))]
[JsonDerivedType(typeof(TMessageActionSetChatTheme), nameof(TMessageActionSetChatTheme))]
[JsonDerivedType(typeof(TMessageActionChatJoinedByRequest), nameof(TMessageActionChatJoinedByRequest))]
[JsonDerivedType(typeof(TMessageActionWebViewDataSentMe), nameof(TMessageActionWebViewDataSentMe))]
[JsonDerivedType(typeof(TMessageActionWebViewDataSent), nameof(TMessageActionWebViewDataSent))]
[JsonDerivedType(typeof(TMessageActionGiftPremium), nameof(TMessageActionGiftPremium))]
[JsonDerivedType(typeof(TMessageActionTopicCreate), nameof(TMessageActionTopicCreate))]
[JsonDerivedType(typeof(TMessageActionTopicEdit), nameof(TMessageActionTopicEdit))]
[JsonDerivedType(typeof(TMessageActionSuggestProfilePhoto), nameof(TMessageActionSuggestProfilePhoto))]
[JsonDerivedType(typeof(TMessageActionRequestedPeer), nameof(TMessageActionRequestedPeer))]
[JsonDerivedType(typeof(TMessageActionSetChatWallPaper), nameof(TMessageActionSetChatWallPaper))]
[JsonDerivedType(typeof(TMessageActionSetSameChatWallPaper), nameof(TMessageActionSetSameChatWallPaper))]
[JsonDerivedType(typeof(TMessageActionGiftCode), nameof(TMessageActionGiftCode))]
[JsonDerivedType(typeof(TMessageActionGiveawayLaunch), nameof(TMessageActionGiveawayLaunch))]
public interface IMessageAction : IObject
{

}
