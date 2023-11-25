// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Privacy key
/// See <a href="https://corefork.telegram.org/constructor/InputPrivacyKey" />
///</summary>
[JsonDerivedType(typeof(TInputPrivacyKeyStatusTimestamp), nameof(TInputPrivacyKeyStatusTimestamp))]
[JsonDerivedType(typeof(TInputPrivacyKeyChatInvite), nameof(TInputPrivacyKeyChatInvite))]
[JsonDerivedType(typeof(TInputPrivacyKeyPhoneCall), nameof(TInputPrivacyKeyPhoneCall))]
[JsonDerivedType(typeof(TInputPrivacyKeyPhoneP2P), nameof(TInputPrivacyKeyPhoneP2P))]
[JsonDerivedType(typeof(TInputPrivacyKeyForwards), nameof(TInputPrivacyKeyForwards))]
[JsonDerivedType(typeof(TInputPrivacyKeyProfilePhoto), nameof(TInputPrivacyKeyProfilePhoto))]
[JsonDerivedType(typeof(TInputPrivacyKeyPhoneNumber), nameof(TInputPrivacyKeyPhoneNumber))]
[JsonDerivedType(typeof(TInputPrivacyKeyAddedByPhone), nameof(TInputPrivacyKeyAddedByPhone))]
[JsonDerivedType(typeof(TInputPrivacyKeyVoiceMessages), nameof(TInputPrivacyKeyVoiceMessages))]
[JsonDerivedType(typeof(TInputPrivacyKeyAbout), nameof(TInputPrivacyKeyAbout))]
public interface IInputPrivacyKey : IObject
{

}
