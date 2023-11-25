// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains info on group members.
/// See <a href="https://corefork.telegram.org/constructor/ChatParticipants" />
///</summary>
[JsonDerivedType(typeof(TChatParticipantsForbidden), nameof(TChatParticipantsForbidden))]
[JsonDerivedType(typeof(TChatParticipants), nameof(TChatParticipants))]
public interface IChatParticipants : IObject
{
    ///<summary>
    /// Group identifier
    ///</summary>
    long ChatId { get; set; }
}
