// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// How a user voted in a poll
/// See <a href="https://corefork.telegram.org/constructor/MessageUserVote" />
///</summary>
public interface IMessageUserVote : IObject
{
    ///<summary>
    /// User ID
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// When did the user cast their votes
    ///</summary>
    int Date { get; set; }
}
