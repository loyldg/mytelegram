// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stories.StoryReactionsList" />
///</summary>
[JsonDerivedType(typeof(TStoryReactionsList), nameof(TStoryReactionsList))]
public interface IStoryReactionsList : IObject
{
    BitArray Flags { get; set; }
    int Count { get; set; }
    TVector<MyTelegram.Schema.IStoryReaction> Reactions { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
    string? NextOffset { get; set; }
}
