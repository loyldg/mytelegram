// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Privacy <strong>rules</strong> indicate <em>who</em> can or can't do something and are specified by a <a href="https://corefork.telegram.org/type/PrivacyRule">PrivacyRule</a>, and its input counterpart <a href="https://corefork.telegram.org/type/InputPrivacyRule">InputPrivacyRule</a>.See the <a href="https://corefork.telegram.org/api/privacy">privacy documentation »</a> for more info.
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
