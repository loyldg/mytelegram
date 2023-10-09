// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Delete messages in a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 MESSAGE_DELETE_FORBIDDEN You can't delete one of the messages you tried to delete, most likely because it is a service message.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.deleteMessages" />
///</summary>
internal sealed class DeleteMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteMessages, MyTelegram.Schema.Messages.IAffectedMessages>,
    Channels.IDeleteMessagesHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteMessages obj)
    {
        throw new NotImplementedException();
    }
}
