namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Edits notification settings from a given user/group, from all users/all groups.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SETTINGS_INVALID Invalid settings were provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateNotifySettings"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateNotifySettingsHandler(ICommandBus commandBus) : RpcResultObjectHandler<RequestUpdateNotifySettings, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, RequestUpdateNotifySettings obj)
    {
        PeerNotifyType peerNotifyType = PeerNotifyType.Unknown;
        long toPeerId = 0;
        switch (obj.Peer)
        {
            case TInputNotifyBroadcasts:
                peerNotifyType = PeerNotifyType.Broadcasts;
                break;
            case TInputNotifyChats:
                peerNotifyType = PeerNotifyType.Chats;
                break;
            case TInputNotifyForumTopic:
                peerNotifyType = PeerNotifyType.ForumTopic;
                break;
            case TInputNotifyPeer inputNotifyPeer1:
                peerNotifyType = PeerNotifyType.Peer;
                var peer = inputNotifyPeer1.Peer.ToPeer(input.UserId);
                toPeerId = peer.PeerId;
                break;
            case TInputNotifyUsers:
                peerNotifyType = PeerNotifyType.Users;
                break;
        }

        var userId = input.UserId;
        var id = PeerNotifySettingsId.Create2(userId, peerNotifyType, toPeerId);
        var peerNotifySettings = new TPeerNotifySettings
        {
            AndroidSound = obj.Settings.Sound,
            IosSound = obj.Settings.Sound,
            StoriesAndroidSound = obj.Settings.StoriesSound,
            OtherSound = obj.Settings.Sound,
            StoriesIosSound = obj.Settings.StoriesSound,
            StoriesOtherSound = obj.Settings.Sound,
            StoriesHideSender = obj.Settings.StoriesHideSender,
            MuteUntil = obj.Settings.MuteUntil,
            StoriesMuted = obj.Settings.StoriesMuted,
            ShowPreviews = obj.Settings.ShowPreviews,
            Silent = obj.Settings.Silent,
        };
        var command = new UpdatePeerNotifySettingsCommand2(id, input.ToRequestInfo(), input.UserId, peerNotifyType,
            toPeerId, peerNotifySettings);
        await commandBus.PublishAsync(command);

        return new TBoolTrue();
    }
}