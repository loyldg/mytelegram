// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/forum">topics of a forum</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_FORUM_MISSING This supergroup is not a forum.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.getForumTopics" />
///</summary>
internal sealed class GetForumTopicsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetForumTopics, MyTelegram.Schema.Messages.IForumTopics>,
    Channels.IGetForumTopicsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IForumTopics> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetForumTopics obj)
    {
        throw new NotImplementedException();
    }
}
