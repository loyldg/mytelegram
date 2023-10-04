// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates a possible answer to a <a href="https://corefork.telegram.org/type/Poll">poll</a>.
/// See <a href="https://corefork.telegram.org/constructor/PollAnswer" />
///</summary>
public interface IPollAnswer : IObject
{
    ///<summary>
    /// Textual representation of the answer
    ///</summary>
    string Text { get; set; }

    ///<summary>
    /// The param that has to be passed to <a href="https://corefork.telegram.org/method/messages.sendVote">messages.sendVote</a>.
    ///</summary>
    string Option { get; set; }
}
