// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Privacy <strong>keys</strong> together with <a href="https://corefork.telegram.org/api/privacy#privacy-rules">privacy rules »</a> indicate <em>what</em> can or can't someone do and are specified by a <a href="https://corefork.telegram.org/type/PrivacyKey">PrivacyKey</a> constructor, and its input counterpart <a href="https://corefork.telegram.org/type/InputPrivacyKey">InputPrivacyKey</a>.See the <a href="https://corefork.telegram.org/api/privacy">privacy documentation »</a> for more info.
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
