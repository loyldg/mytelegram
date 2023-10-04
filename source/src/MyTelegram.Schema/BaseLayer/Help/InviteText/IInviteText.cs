// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Object contains info on the text of a message with an invitation.
/// See <a href="https://corefork.telegram.org/constructor/help.InviteText" />
///</summary>
public interface IInviteText : IObject
{
    ///<summary>
    /// Text of the message
    ///</summary>
    string Message { get; set; }
}
