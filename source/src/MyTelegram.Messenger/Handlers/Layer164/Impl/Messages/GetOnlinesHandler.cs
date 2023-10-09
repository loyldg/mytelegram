// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get count of online users in a chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getOnlines" />
///</summary>
internal sealed class GetOnlinesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetOnlines, MyTelegram.Schema.IChatOnlines>,
    Messages.IGetOnlinesHandler
{
    protected override Task<MyTelegram.Schema.IChatOnlines> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetOnlines obj)
    {
        throw new NotImplementedException();
    }
}
