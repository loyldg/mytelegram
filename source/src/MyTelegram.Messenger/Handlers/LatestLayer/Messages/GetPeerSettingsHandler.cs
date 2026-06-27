namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get peer settings
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_MONOFORUM_UNSUPPORTED <a href="https://corefork.telegram.org/api/channel#monoforums">Monoforums</a> do not support this feature.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getPeerSettings"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPeerSettingsHandler(IPeerSettingsAppService peerSettingsAppService, IPeerHelper peerHelper, IObjectMapper objectMapper, IQueryProcessor queryProcessor, IContactAppService contactAppService, IChannelAppService channelAppService, ILayeredService<IPeerSettingsConverter> layeredService) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPeerSettings, MyTelegram.Schema.Messages.IPeerSettings>
{
    protected override async Task<MyTelegram.Schema.Messages.IPeerSettings> HandleCoreAsync(IRequestInput input, RequestGetPeerSettings obj)
    {
        var userId = input.UserId;
        var peer = peerHelper.GetPeer(obj.Peer, userId);
        if (peer.PeerType == PeerType.Channel)
        {
            if (await channelAppService.SendRpcErrorIfNotChannelMemberAsync(input, peer.PeerId))
            {
                return null!;
            }
        }

        if (peer.PeerId == MyTelegramConsts.NotificationServiceUserId || peer.PeerType == PeerType.Self)
        {
            return new MyTelegram.Schema.Messages.TPeerSettings
            {
                Chats = new(),
                Users = new(),
                Settings = new Schema.TPeerSettings()
            };
        }

        //IContactReadModel? contactReadModel = null;
        ContactType? contactType = null;
        if (peer.PeerType == PeerType.User)
        {
            //contactReadModel = await _queryProcessor.ProcessAsync(new GetContactQuery(userId, peer.PeerId));
            var contactReadModels = await queryProcessor.ProcessAsync(new GetContactListBySelfIdAndTargetUserIdQuery(input.UserId, peer.PeerId));
            contactType = contactAppService.GetContactType(input.UserId, peer.PeerId, contactReadModels);
        }

        var r = await peerSettingsAppService.GetPeerSettingsAsync(userId, peer.PeerId);
        var settings = layeredService.GetConverter(input.Layer).ToPeerSettings(input.UserId, peer.PeerId, r, contactType);
        if (r == null && peer.PeerType == PeerType.Channel)
        {
            settings = new MyTelegram.Schema.TPeerSettings();
        }

        var peerSettings = new MyTelegram.Schema.Messages.TPeerSettings
        {
            Chats = new TVector<IChat>(),
            Users = new TVector<IUser>(),
            Settings = settings
        };
        return peerSettings;
    }
}