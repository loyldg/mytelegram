// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Results of poll
/// See <a href="https://corefork.telegram.org/constructor/PollResults" />
///</summary>
public interface IPollResults : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Similar to <a href="https://corefork.telegram.org/api/min">min</a> objects, used for poll constructors that are the same for all users so they don't have the option chosen by the current user (you can use <a href="https://corefork.telegram.org/method/messages.getPollResults">messages.getPollResults</a> to get the full poll results).
    ///</summary>
    bool Min { get; set; }

    ///<summary>
    /// Poll results
    /// See <a href="https://corefork.telegram.org/type/PollAnswerVoters" />
    ///</summary>
    TVector<MyTelegram.Schema.IPollAnswerVoters>? Results { get; set; }

    ///<summary>
    /// Total number of people that voted in the poll
    ///</summary>
    int? TotalVoters { get; set; }

    ///<summary>
    /// IDs of the last users that recently voted in the poll
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    TVector<MyTelegram.Schema.IPeer>? RecentVoters { get; set; }

    ///<summary>
    /// Explanation of quiz solution
    ///</summary>
    string? Solution { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/entities">Message entities for styled text in quiz solution</a>
    /// See <a href="https://corefork.telegram.org/type/MessageEntity" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageEntity>? SolutionEntities { get; set; }
}
