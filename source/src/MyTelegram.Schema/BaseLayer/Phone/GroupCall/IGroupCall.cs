// ReSharper disable All

namespace MyTelegram.Schema.Phone;

///<summary>
/// Contains info about a group call, and partial info about its participants.
/// See <a href="https://corefork.telegram.org/constructor/phone.GroupCall" />
///</summary>
public interface IGroupCall : IObject
{
    ///<summary>
    /// Info about the group call
    /// See <a href="https://corefork.telegram.org/type/GroupCall" />
    ///</summary>
    MyTelegram.Schema.IGroupCall Call { get; set; }

    ///<summary>
    /// A partial list of participants.
    /// See <a href="https://corefork.telegram.org/type/GroupCallParticipant" />
    ///</summary>
    TVector<MyTelegram.Schema.IGroupCallParticipant> Participants { get; set; }

    ///<summary>
    /// Next offset to use when fetching the remaining participants using <a href="https://corefork.telegram.org/method/phone.getGroupParticipants">phone.getGroupParticipants</a>
    ///</summary>
    string ParticipantsNextOffset { get; set; }

    ///<summary>
    /// Chats mentioned in the participants vector
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users mentioned in the participants vector
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
