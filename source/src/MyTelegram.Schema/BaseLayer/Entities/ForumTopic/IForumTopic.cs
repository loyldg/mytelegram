// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains information about a <a href="https://corefork.telegram.org/api/forum#forum-topics">forum topic</a>
/// See <a href="https://corefork.telegram.org/constructor/ForumTopic" />
///</summary>
public interface IForumTopic : IObject
{
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/forum#forum-topics">Topic ID</a>
    ///</summary>
    int Id { get; set; }
}
