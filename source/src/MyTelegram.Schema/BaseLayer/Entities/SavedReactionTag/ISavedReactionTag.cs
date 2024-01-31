// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/SavedReactionTag" />
///</summary>
[JsonDerivedType(typeof(TSavedReactionTag), nameof(TSavedReactionTag))]
public interface ISavedReactionTag : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IReaction Reaction { get; set; }
    string? Title { get; set; }
    int Count { get; set; }
}
