// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stories.StoryViews" />
///</summary>
public interface IStoryViews : IObject
{
    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/StoryViews" />
    ///</summary>
    TVector<MyTelegram.Schema.IStoryViews> Views { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
