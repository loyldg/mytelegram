namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Send scheduled messages right away
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.sendScheduledMessages" />
///</summary>
internal sealed class SendScheduledMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendScheduledMessages, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendScheduledMessages obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Updates = [],
            Chats = [],
            Users = [],
            Date = CurrentDate
        });
    }
}
