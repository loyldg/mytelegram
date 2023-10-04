// ReSharper disable All

namespace MyTelegram.Schema.Phone;

///<summary>
/// Info about the participants of a group call or livestream
/// See <a href="https://corefork.telegram.org/constructor/phone.GroupParticipants" />
///</summary>
public interface IGroupParticipants : IObject
{
    ///<summary>
    /// Number of participants
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// List of participants
    /// See <a href="https://corefork.telegram.org/type/GroupCallParticipant" />
    ///</summary>
    TVector<MyTelegram.Schema.IGroupCallParticipant> Participants { get; set; }

    ///<summary>
    /// If not empty, the specified list of participants is partial, and more participants can be fetched specifying this parameter as <code>offset</code> in <a href="https://corefork.telegram.org/method/phone.getGroupParticipants">phone.getGroupParticipants</a>.
    ///</summary>
    string NextOffset { get; set; }

    ///<summary>
    /// Mentioned chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

    ///<summary>
    /// Version info
    ///</summary>
    int Version { get; set; }
}
