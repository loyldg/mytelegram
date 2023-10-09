// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get forum topics by their ID
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_FORUM_MISSING This supergroup is not a forum.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 TOPICS_EMPTY &nbsp;
/// See <a href="https://corefork.telegram.org/method/channels.getForumTopicsByID" />
///</summary>
internal sealed class GetForumTopicsByIDHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetForumTopicsByID, MyTelegram.Schema.Messages.IForumTopics>,
    Channels.IGetForumTopicsByIDHandler
{
    protected override Task<MyTelegram.Schema.Messages.IForumTopics> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetForumTopicsByID obj)
    {
        throw new NotImplementedException();
    }
}
