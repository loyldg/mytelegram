// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/PostInteractionCounters" />
///</summary>
[JsonDerivedType(typeof(TPostInteractionCountersMessage), nameof(TPostInteractionCountersMessage))]
[JsonDerivedType(typeof(TPostInteractionCountersStory), nameof(TPostInteractionCountersStory))]
public interface IPostInteractionCounters : IObject
{
    ///<summary>
    /// &nbsp;
    ///</summary>
    int Views { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int Forwards { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int Reactions { get; set; }
}
