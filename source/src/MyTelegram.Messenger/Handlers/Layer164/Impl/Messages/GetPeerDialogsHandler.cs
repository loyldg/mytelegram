// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get dialog info of specified peers
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getPeerDialogs" />
///</summary>
internal sealed class GetPeerDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPeerDialogs, MyTelegram.Schema.Messages.IPeerDialogs>,
    Messages.IGetPeerDialogsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IPeerDialogs> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetPeerDialogs obj)
    {
        throw new NotImplementedException();
    }
}
