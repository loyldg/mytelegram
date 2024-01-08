// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// Reaction and view counters for a list of <a href="https://corefork.telegram.org/api/stories">stories</a>
/// See <a href="https://corefork.telegram.org/constructor/stories.StoryViews" />
///</summary>
[JsonDerivedType(typeof(TStoryViews), nameof(TStoryViews))]
public interface IStoryViews : IObject
{
    ///<summary>
    /// View date and reaction information of multiple stories
    /// See <a href="https://corefork.telegram.org/type/StoryViews" />
    ///</summary>
    TVector<MyTelegram.Schema.IStoryViews> Views { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
