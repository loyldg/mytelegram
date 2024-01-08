// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/api/stories">Telegram Story</a>
/// See <a href="https://corefork.telegram.org/constructor/StoryItem" />
///</summary>
[JsonDerivedType(typeof(TStoryItemDeleted), nameof(TStoryItemDeleted))]
[JsonDerivedType(typeof(TStoryItemSkipped), nameof(TStoryItemSkipped))]
[JsonDerivedType(typeof(TStoryItem), nameof(TStoryItem))]
public interface IStoryItem : IObject
{
    ///<summary>
    /// ID of the story.
    ///</summary>
    int Id { get; set; }
}
