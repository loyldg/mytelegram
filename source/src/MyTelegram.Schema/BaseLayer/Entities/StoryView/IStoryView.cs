// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/stories">Story</a> view date and reaction information
/// See <a href="https://corefork.telegram.org/constructor/StoryView" />
///</summary>
[JsonDerivedType(typeof(TStoryView), nameof(TStoryView))]
[JsonDerivedType(typeof(TStoryViewPublicForward), nameof(TStoryViewPublicForward))]
[JsonDerivedType(typeof(TStoryViewPublicRepost), nameof(TStoryViewPublicRepost))]
public interface IStoryView : IObject
{
    BitArray Flags { get; set; }
    bool Blocked { get; set; }
    bool BlockedMyStoriesFrom { get; set; }
}
