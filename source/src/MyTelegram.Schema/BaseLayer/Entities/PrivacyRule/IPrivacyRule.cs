// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Privacy rule
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
