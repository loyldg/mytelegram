// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Privacy <strong>keys</strong> together with <a href="https://corefork.telegram.org/api/privacy#privacy-rules">privacy rules »</a> indicate <em>what</em> can or can't someone do and are specified by a <a href="https://corefork.telegram.org/type/PrivacyKey">PrivacyKey</a> constructor, and its input counterpart <a href="https://corefork.telegram.org/type/InputPrivacyKey">InputPrivacyKey</a>.See the <a href="https://corefork.telegram.org/api/privacy">privacy documentation »</a> for more info.
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
