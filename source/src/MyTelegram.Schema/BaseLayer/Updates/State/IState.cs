// ReSharper disable All

namespace MyTelegram.Schema.Updates;

///<summary>
/// Object contains info on state for further updates.
/// See <a href="https://corefork.telegram.org/constructor/updates.State" />
///</summary>
public interface IState : IObject
{
    ///<summary>
    /// Number of events occurred in a text box
    ///</summary>
    int Pts { get; set; }

    ///<summary>
    /// Position in a sequence of updates in secret chats. For further details refer to article <a href="https://corefork.telegram.org/api/end-to-end">secret chats</a>
    ///</summary>
    int Qts { get; set; }

    ///<summary>
    /// Date of condition
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// Number of sent updates
    ///</summary>
    int Seq { get; set; }

    ///<summary>
    /// Number of unread messages
    ///</summary>
    int UnreadCount { get; set; }
}
