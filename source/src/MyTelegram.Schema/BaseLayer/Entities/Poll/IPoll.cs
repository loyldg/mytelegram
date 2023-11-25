// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates a poll message
/// See <a href="https://corefork.telegram.org/constructor/Poll" />
///</summary>
[JsonDerivedType(typeof(TPoll), nameof(TPoll))]
public interface IPoll : IObject
{
    ///<summary>
    /// ID of the poll
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the poll is closed and doesn't accept any more answers
    ///</summary>
    bool Closed { get; set; }

    ///<summary>
    /// Whether cast votes are publicly visible to all users (non-anonymous poll)
    ///</summary>
    bool PublicVoters { get; set; }

    ///<summary>
    /// Whether multiple options can be chosen as answer
    ///</summary>
    bool MultipleChoice { get; set; }

    ///<summary>
    /// Whether this is a quiz (with wrong and correct answers, results shown in the return type)
    ///</summary>
    bool Quiz { get; set; }

    ///<summary>
    /// The question of the poll
    ///</summary>
    string Question { get; set; }

    ///<summary>
    /// The possible answers, vote using <a href="https://corefork.telegram.org/method/messages.sendVote">messages.sendVote</a>.
    /// See <a href="https://corefork.telegram.org/type/PollAnswer" />
    ///</summary>
    TVector<MyTelegram.Schema.IPollAnswer> Answers { get; set; }

    ///<summary>
    /// Amount of time in seconds the poll will be active after creation, 5-600. Can't be used together with close_date.
    ///</summary>
    int? ClosePeriod { get; set; }

    ///<summary>
    /// Point in time (Unix timestamp) when the poll will be automatically closed. Must be at least 5 and no more than 600 seconds in the future; can't be used together with close_period.
    ///</summary>
    int? CloseDate { get; set; }
}
