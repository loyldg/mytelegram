// ReSharper disable All

using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get info about the chat invites of a specific chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ADMIN_ID_INVALID The specified admin ID is invalid.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getExportedChatInvites" />
///</summary>
internal sealed class GetExportedChatInvitesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetExportedChatInvites, MyTelegram.Schema.Messages.IExportedChatInvites>,
    Messages.IGetExportedChatInvitesHandler
{
    private readonly IPeerHelper _peerHelper;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IObjectMapper _objectMapper;
    private readonly IPhotoAppService _photoAppService;
    private readonly IPrivacyAppService _privacyAppService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;

    public GetExportedChatInvitesHandler(IPeerHelper peerHelper,
        IOptions<MyTelegramMessengerServerOptions> options,
        IAccessHashHelper accessHashHelper, IQueryProcessor queryProcessor, IObjectMapper objectMapper, IPhotoAppService photoAppService, IPrivacyAppService privacyAppService, ILayeredService<IUserConverter> layeredUserService)
    {
        _peerHelper = peerHelper;
        _options = options;
        _accessHashHelper = accessHashHelper;
        _queryProcessor = queryProcessor;
        _objectMapper = objectMapper;
        _photoAppService = photoAppService;
        _privacyAppService = privacyAppService;
        _layeredUserService = layeredUserService;
    }

    protected async override Task<MyTelegram.Schema.Messages.IExportedChatInvites> HandleCoreAsync(IRequestInput input,
        RequestGetExportedChatInvites obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        await _accessHashHelper.CheckAccessHashAsync(obj.AdminId);

        // todo:impl get chat invites
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var channelReadModel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(peer.PeerId), default);
        if (channelReadModel == null)
        {
            RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
        }

        if (!channelReadModel!.AdminList.Any(p => p.UserId == input.UserId))
        {
            RpcErrors.RpcErrors400.ChatAdminRequired.ThrowRpcError();
        }

        var admin = _peerHelper.GetPeer(obj.AdminId, input.UserId);
        //return new TExportedChatInvites
        //{
        //    Count = 1,
        //    Users = new TVector<IUser>(),
        //    Invites = new TVector<IExportedChatInvite> {
        //        new TChatInviteExported {
        //            AdminId = admin.PeerId,
        //            Date = CurrentDate,
        //            ExpireDate = DateTime.UtcNow.AddDays(30).ToTimestamp(),
        //            Link =
        //                $"{_options.Value.JoinChatDomain}/AAAAA{peer.PeerId}/{_randomHelper.GenerateRandomString(8)}",
        //            Permanent = true,
        //            Revoked = false,
        //            StartDate = CurrentDate,
        //            Usage = 0,
        //            UsageLimit = 0
        //        }
        //    }
        //};



        var invites = await _queryProcessor
            .ProcessAsync(new GetChatInvitesQuery(obj.Revoked,
                    peer.PeerId,
                    admin.PeerId,
                    obj.OffsetDate,
                    obj.OffsetLink,
                    obj.Limit),default);
        var userIds = invites.Select(p => p.AdminId).ToList();
        var userReadModels = await _queryProcessor.ProcessAsync(new GetUsersByUidListQuery(userIds), default);
        var contactReadModels = new List<IContactReadModel>();
        var photoReadModels = await _photoAppService.GetPhotosAsync(userReadModels, contactReadModels);
        var privacyReadModels = await _privacyAppService.GetPrivacyListAsync(userIds);
        var users = _layeredUserService.GetConverter(input.Layer).ToUserList(input.UserId, userReadModels,
            photoReadModels, contactReadModels, privacyReadModels);

        var tInvites = invites.Select(p => _objectMapper.Map<IChatInviteReadModel, TChatInviteExported>(p)).ToList();
        tInvites.ForEach(p => p.Link = $"{_options.Value.JoinChatDomain}/+{p.Link}");

        var r = new TExportedChatInvites
        {
            Count = invites.Count,
            Invites = new TVector<IExportedChatInvite>(tInvites),
            Users = new TVector<IUser>(users),
        };

        return r;
    }
}
