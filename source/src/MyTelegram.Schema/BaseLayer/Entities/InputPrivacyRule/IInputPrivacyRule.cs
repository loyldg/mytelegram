// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Privacy rule
/// See <a href="https://corefork.telegram.org/constructor/InputPrivacyRule" />
///</summary>
[JsonDerivedType(typeof(TInputPrivacyValueAllowContacts), nameof(TInputPrivacyValueAllowContacts))]
[JsonDerivedType(typeof(TInputPrivacyValueAllowAll), nameof(TInputPrivacyValueAllowAll))]
[JsonDerivedType(typeof(TInputPrivacyValueAllowUsers), nameof(TInputPrivacyValueAllowUsers))]
[JsonDerivedType(typeof(TInputPrivacyValueDisallowContacts), nameof(TInputPrivacyValueDisallowContacts))]
[JsonDerivedType(typeof(TInputPrivacyValueDisallowAll), nameof(TInputPrivacyValueDisallowAll))]
[JsonDerivedType(typeof(TInputPrivacyValueDisallowUsers), nameof(TInputPrivacyValueDisallowUsers))]
[JsonDerivedType(typeof(TInputPrivacyValueAllowChatParticipants), nameof(TInputPrivacyValueAllowChatParticipants))]
[JsonDerivedType(typeof(TInputPrivacyValueDisallowChatParticipants), nameof(TInputPrivacyValueDisallowChatParticipants))]
[JsonDerivedType(typeof(TInputPrivacyValueAllowCloseFriends), nameof(TInputPrivacyValueAllowCloseFriends))]
public interface IInputPrivacyRule : IObject
{

}
