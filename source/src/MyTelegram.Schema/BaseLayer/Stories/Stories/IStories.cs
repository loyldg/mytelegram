// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stories.Stories" />
///</summary>
[JsonDerivedType(typeof(TStories), nameof(TStories))]
public interface IStories : IObject
{
    ///<summary>
    /// Total number of stories that can be fetched
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// Stories
    /// See <a href="https://corefork.telegram.org/type/StoryItem" />
    ///</summary>
    TVector<MyTelegram.Schema.IStoryItem> Stories { get; set; }

    ///<summary>
    /// Mentioned chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
