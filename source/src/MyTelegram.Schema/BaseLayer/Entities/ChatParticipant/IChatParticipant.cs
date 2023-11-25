// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Details of a group member.
/// See <a href="https://corefork.telegram.org/constructor/ChatParticipant" />
///</summary>
[JsonDerivedType(typeof(TChatParticipant), nameof(TChatParticipant))]
[JsonDerivedType(typeof(TChatParticipantCreator), nameof(TChatParticipantCreator))]
[JsonDerivedType(typeof(TChatParticipantAdmin), nameof(TChatParticipantAdmin))]
public interface IChatParticipant : IObject
{
    ///<summary>
    /// ID of a group member that is admin
    ///</summary>
    long UserId { get; set; }
}
