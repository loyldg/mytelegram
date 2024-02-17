// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get info about the users that joined the chat using a specific chat invite
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SEARCH_WITH_LINK_NOT_SUPPORTED You cannot provide a search query and an invite link at the same time.
/// See <a href="https://corefork.telegram.org/method/messages.getChatInviteImporters" />
///</summary>
internal sealed class GetChatInviteImportersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetChatInviteImporters, MyTelegram.Schema.Messages.IChatInviteImporters>,
    Messages.IGetChatInviteImportersHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPeerHelper _peerHelper;
    private readonly IPhotoAppService _photoAppService;
    private readonly IPrivacyAppService _privacyAppService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    public GetChatInviteImportersHandler(IQueryProcessor queryProcessor, IAccessHashHelper accessHashHelper, IPeerHelper peerHelper, IPhotoAppService photoAppService, IPrivacyAppService privacyAppService, ILayeredService<IUserConverter> layeredUserService)
    {
        _queryProcessor = queryProcessor;
        _accessHashHelper = accessHashHelper;
        _peerHelper = peerHelper;
        _photoAppService = photoAppService;
        _privacyAppService = privacyAppService;
        _layeredUserService = layeredUserService;
    }

    protected override async Task<MyTelegram.Schema.Messages.IChatInviteImporters> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetChatInviteImporters obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputPeerChannel);
            var userPeer = _peerHelper.GetPeer(obj.OffsetUser);

            var channelAdminReadModel = await _queryProcessor.ProcessAsync(new GetChatAdminQuery(inputPeerChannel.ChannelId, input.UserId));
            if (channelAdminReadModel == null)
            {
                RpcErrors.RpcErrors403.ChatAdminRequired.ThrowRpcError();
            }

            var inviteImporterReadModels = await _queryProcessor.ProcessAsync(
                new GetChatInviteImportersQuery(inputPeerChannel.ChannelId, obj.Requested ? ChatInviteRequestState.NeedApprove : null, 0, obj.OffsetDate,
                    userPeer.PeerId, obj.Q, obj.Limit));

            // only support layer 158+
            var importers = new List<TChatInviteImporter>();
            var userIds = new List<long>();
            foreach (var readModel in inviteImporterReadModels)
            {
                var importer = new TChatInviteImporter
                {
                    About = readModel.About,
                    ApprovedBy = readModel.ApprovedBy,
                    Date = readModel.Date,
                    Requested = readModel.ChatInviteRequestState == ChatInviteRequestState.NeedApprove,
                    UserId = readModel.UserId,
                    //ViaChatlist = readModel.ViaChatList
                };
                importers.Add(importer);
                userIds.Add(readModel.UserId);
            }

            var userReadModels = await _queryProcessor.ProcessAsync(new GetUsersByUidListQuery(userIds));
            var contactReadModels = await _queryProcessor.ProcessAsync(new GetContactListQuery(input.UserId, userIds));
            var photoReadModes = await _photoAppService.GetPhotosAsync(userReadModels, contactReadModels);
            var privacyReadModels = await _privacyAppService.GetPrivacyListAsync(userIds);

            var users = _layeredUserService.GetConverter(input.Layer).ToUserList(input.UserId, userReadModels,
                photoReadModes, contactReadModels, privacyReadModels);

            return new TChatInviteImporters
            {
                Importers = new(importers),
                Users = new(users),
            };
        }

        return new TChatInviteImporters
        {
            Importers = new(),
            Users = new(),
        };
    }
}
