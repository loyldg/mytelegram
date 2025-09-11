using MyTelegram.Domain.Aggregates.UserConfig;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Change the default peer that should be used when sending messages, reactions, poll votes to a specific group
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SEND_AS_PEER_INVALID You can't send messages as the specified peer.
/// See <a href="https://corefork.telegram.org/method/messages.saveDefaultSendAs" />
///</summary>
internal sealed class SaveDefaultSendAsHandler(ICommandBus commandBus, IAccessHashHelper accessHashHelper, IPeerHelper peerHelper, IMessageAppService messageAppService) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSaveDefaultSendAs, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSaveDefaultSendAs obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        await accessHashHelper.CheckAccessHashAsync(input, obj.SendAs);
        var peer = peerHelper.GetPeer(obj.Peer);
        var sendAsPeer = peerHelper.GetPeer(obj.SendAs, input.UserId);
        await messageAppService.CheckSendAsAsync(input.UserId, peer, sendAsPeer);
        var key = ((int)UserConfigType.SendAsPeer).ToString();
        var command = new UpdateUserConfigCommand(
            UserConfigId.Create(input.UserId, key), input.ToRequestInfo(),
            input.UserId, key, sendAsPeer.PeerId.ToString());
        await commandBus.PublishAsync(command);

        return new TBoolTrue();
    }
}
