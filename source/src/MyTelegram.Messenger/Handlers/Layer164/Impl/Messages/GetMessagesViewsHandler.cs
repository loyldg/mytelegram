// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get and increase the view counter of a message sent or forwarded from a <a href="https://corefork.telegram.org/api/channel">channel</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getMessagesViews" />
///</summary>
internal sealed class GetMessagesViewsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessagesViews, MyTelegram.Schema.Messages.IMessageViews>,
    Messages.IGetMessagesViewsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessageViews> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetMessagesViews obj)
    {
        throw new NotImplementedException();
    }
}
