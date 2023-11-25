// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Privacy key
/// See <a href="https://corefork.telegram.org/constructor/PrivacyKey" />
///</summary>
[JsonDerivedType(typeof(TPrivacyKeyStatusTimestamp), nameof(TPrivacyKeyStatusTimestamp))]
[JsonDerivedType(typeof(TPrivacyKeyChatInvite), nameof(TPrivacyKeyChatInvite))]
[JsonDerivedType(typeof(TPrivacyKeyPhoneCall), nameof(TPrivacyKeyPhoneCall))]
[JsonDerivedType(typeof(TPrivacyKeyPhoneP2P), nameof(TPrivacyKeyPhoneP2P))]
[JsonDerivedType(typeof(TPrivacyKeyForwards), nameof(TPrivacyKeyForwards))]
[JsonDerivedType(typeof(TPrivacyKeyProfilePhoto), nameof(TPrivacyKeyProfilePhoto))]
[JsonDerivedType(typeof(TPrivacyKeyPhoneNumber), nameof(TPrivacyKeyPhoneNumber))]
[JsonDerivedType(typeof(TPrivacyKeyAddedByPhone), nameof(TPrivacyKeyAddedByPhone))]
[JsonDerivedType(typeof(TPrivacyKeyVoiceMessages), nameof(TPrivacyKeyVoiceMessages))]
[JsonDerivedType(typeof(TPrivacyKeyAbout), nameof(TPrivacyKeyAbout))]
public interface IPrivacyKey : IObject
{

}
