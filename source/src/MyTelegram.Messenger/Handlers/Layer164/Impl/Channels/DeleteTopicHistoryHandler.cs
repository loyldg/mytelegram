// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Delete message history of a <a href="https://corefork.telegram.org/api/forum">forum topic</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 TOPIC_ID_INVALID The specified topic ID is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.deleteTopicHistory" />
///</summary>
internal sealed class DeleteTopicHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteTopicHistory, MyTelegram.Schema.Messages.IAffectedHistory>,
    Channels.IDeleteTopicHistoryHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteTopicHistory obj)
    {
        throw new NotImplementedException();
    }
}
