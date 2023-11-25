// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// How users voted on a certain poll answer
/// See <a href="https://corefork.telegram.org/constructor/PollAnswerVoters" />
///</summary>
[JsonDerivedType(typeof(TPollAnswerVoters), nameof(TPollAnswerVoters))]
public interface IPollAnswerVoters : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether we have chosen this answer
    ///</summary>
    bool Chosen { get; set; }

    ///<summary>
    /// For quizzes, whether the option we have chosen is correct
    ///</summary>
    bool Correct { get; set; }

    ///<summary>
    /// The param that has to be passed to <a href="https://corefork.telegram.org/method/messages.sendVote">messages.sendVote</a>.
    ///</summary>
    byte[] Option { get; set; }

    ///<summary>
    /// How many users voted for this option
    ///</summary>
    int Voters { get; set; }
}
