// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Privacy <strong>rules</strong> together with <a href="https://corefork.telegram.org/api/privacy#privacy-keys">privacy keys</a> indicate <em>what</em> can or can't someone do and are specified by a <a href="https://corefork.telegram.org/type/PrivacyRule">PrivacyRule</a> constructor, and its input counterpart <a href="https://corefork.telegram.org/type/InputPrivacyRule">InputPrivacyRule</a>.See the <a href="https://corefork.telegram.org/api/privacy">privacy documentation »</a> for more info.
/// See <a href="https://corefork.telegram.org/constructor/PrivacyRule" />
///</summary>
[JsonDerivedType(typeof(TPrivacyValueAllowContacts), nameof(TPrivacyValueAllowContacts))]
[JsonDerivedType(typeof(TPrivacyValueAllowAll), nameof(TPrivacyValueAllowAll))]
[JsonDerivedType(typeof(TPrivacyValueAllowUsers), nameof(TPrivacyValueAllowUsers))]
[JsonDerivedType(typeof(TPrivacyValueDisallowContacts), nameof(TPrivacyValueDisallowContacts))]
[JsonDerivedType(typeof(TPrivacyValueDisallowAll), nameof(TPrivacyValueDisallowAll))]
[JsonDerivedType(typeof(TPrivacyValueDisallowUsers), nameof(TPrivacyValueDisallowUsers))]
[JsonDerivedType(typeof(TPrivacyValueAllowChatParticipants), nameof(TPrivacyValueAllowChatParticipants))]
[JsonDerivedType(typeof(TPrivacyValueDisallowChatParticipants), nameof(TPrivacyValueDisallowChatParticipants))]
[JsonDerivedType(typeof(TPrivacyValueAllowCloseFriends), nameof(TPrivacyValueAllowCloseFriends))]
public interface IPrivacyRule : IObject
{

}
