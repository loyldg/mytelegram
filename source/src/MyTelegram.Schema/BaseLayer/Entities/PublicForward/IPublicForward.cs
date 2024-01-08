// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains info about the forwards of a <a href="https://corefork.telegram.org/api/stories">story</a> as a message to public chats and reposts by public channels.
/// See <a href="https://corefork.telegram.org/constructor/PublicForward" />
///</summary>
[JsonDerivedType(typeof(TPublicForwardMessage), nameof(TPublicForwardMessage))]
[JsonDerivedType(typeof(TPublicForwardStory), nameof(TPublicForwardStory))]
public interface IPublicForward : IObject
{

}
