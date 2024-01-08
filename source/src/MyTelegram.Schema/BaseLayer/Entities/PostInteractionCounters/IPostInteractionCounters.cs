// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Interaction counters
/// See <a href="https://corefork.telegram.org/constructor/PostInteractionCounters" />
///</summary>
[JsonDerivedType(typeof(TPostInteractionCountersMessage), nameof(TPostInteractionCountersMessage))]
[JsonDerivedType(typeof(TPostInteractionCountersStory), nameof(TPostInteractionCountersStory))]
public interface IPostInteractionCounters : IObject
{
    ///<summary>
    /// Number of views
    ///</summary>
    int Views { get; set; }

    ///<summary>
    /// Number of forwards and reposts to public chats and channels
    ///</summary>
    int Forwards { get; set; }

    ///<summary>
    /// Number of reactions
    ///</summary>
    int Reactions { get; set; }
}
