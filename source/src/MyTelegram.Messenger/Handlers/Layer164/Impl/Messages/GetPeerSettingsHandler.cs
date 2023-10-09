// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get peer settings
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getPeerSettings" />
///</summary>
internal sealed class GetPeerSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPeerSettings, MyTelegram.Schema.Messages.IPeerSettings>,
    Messages.IGetPeerSettingsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IPeerSettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetPeerSettings obj)
    {
        throw new NotImplementedException();
    }
}
