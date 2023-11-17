// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Create a <a href="https://corefork.telegram.org/api/forum">forum topic</a>; requires <a href="https://corefork.telegram.org/api/rights"><code>manage_topics</code> rights</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// See <a href="https://corefork.telegram.org/method/channels.createForumTopic" />
///</summary>
internal sealed class CreateForumTopicHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestCreateForumTopic, MyTelegram.Schema.IUpdates>,
    Channels.ICreateForumTopicHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestCreateForumTopic obj)
    {
        throw new NotImplementedException();
    }
}
