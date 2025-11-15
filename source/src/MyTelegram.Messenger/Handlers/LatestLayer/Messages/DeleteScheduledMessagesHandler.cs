namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Delete scheduled messages
/// Possible errors
/// Code Type Description
/// 403 MESSAGE_DELETE_FORBIDDEN You can't delete one of the messages you tried to delete, most likely because it is a service message.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.deleteScheduledMessages"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteScheduledMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteScheduledMessages, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestDeleteScheduledMessages obj)
    {
        throw new NotImplementedException();
    }
}