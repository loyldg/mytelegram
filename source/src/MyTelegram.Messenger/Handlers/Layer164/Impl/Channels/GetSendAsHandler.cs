// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Obtains a list of peers that can be used to send messages in a specific group
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.getSendAs" />
///</summary>
internal sealed class GetSendAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetSendAs, MyTelegram.Schema.Channels.ISendAsPeers>,
    Channels.IGetSendAsHandler
{
    protected override Task<MyTelegram.Schema.Channels.ISendAsPeers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetSendAs obj)
    {
        throw new NotImplementedException();
    }
}
