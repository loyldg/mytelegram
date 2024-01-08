// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Number of users that reacted with a certain emoji
/// See <a href="https://corefork.telegram.org/constructor/ReactionCount" />
///</summary>
[JsonDerivedType(typeof(TReactionCount), nameof(TReactionCount))]
public interface IReactionCount : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, indicates that the current user also sent this reaction. <br>The integer value indicates when was the reaction added: the bigger the value, the newer the reaction.
    ///</summary>
    int? ChosenOrder { get; set; }

    ///<summary>
    /// The reaction.
    /// See <a href="https://corefork.telegram.org/type/Reaction" />
    ///</summary>
    MyTelegram.Schema.IReaction Reaction { get; set; }

    ///<summary>
    /// Number of users that reacted with this emoji.
    ///</summary>
    int Count { get; set; }
}
