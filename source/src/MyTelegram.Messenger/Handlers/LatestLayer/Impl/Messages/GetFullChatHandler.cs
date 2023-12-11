// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get full info about a <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getFullChat" />
///</summary>
internal sealed class GetFullChatHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFullChat, MyTelegram.Schema.Messages.IChatFull>,
    Messages.IGetFullChatHandler
{
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredChatService;
    private readonly ILayeredService<IPhotoConverter> _layeredPhotoService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly IPhotoAppService _photoAppService;
    private readonly IPrivacyAppService _privacyAppService;

    public GetFullChatHandler(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper,
        ILayeredService<IChatConverter> layeredChatService,
        ILayeredService<IPhotoConverter> layeredPhotoService,
        ILayeredService<IUserConverter> layeredUserService, IPhotoAppService photoAppService, IPrivacyAppService privacyAppService)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _layeredChatService = layeredChatService;
        _layeredPhotoService = layeredPhotoService;
        _layeredUserService = layeredUserService;
        _photoAppService = photoAppService;
        _privacyAppService = privacyAppService;
    }

    protected override async Task<MyTelegram.Schema.Messages.IChatFull> HandleCoreAsync(IRequestInput input,
        RequestGetFullChat obj)
    {
        var peerType = _peerHelper.GetPeerType(obj.ChatId);
        switch (peerType)
        {
            case PeerType.Channel:
                {
                    var channel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(obj.ChatId),
                        CancellationToken.None);
                    var channelFull = await _queryProcessor.ProcessAsync(new GetChannelFullByIdQuery(obj.ChatId),
                        CancellationToken.None);
                    var migratedFromChatReadModel = channelFull!.MigratedFromChatId == null ? null :
                        await _queryProcessor.ProcessAsync(new GetChatByChatIdQuery(channelFull.MigratedFromChatId.Value), default);


                    var channelMember = await _queryProcessor
                        .ProcessAsync(new GetChannelMemberByUidQuery(obj.ChatId, input.UserId), default)
                 ;
                    var peerNotifySettings = await _queryProcessor
                        .ProcessAsync(
                            new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                                PeerType.Channel,
                                obj.ChatId)),
                            CancellationToken.None);
                    var photoReadModel = await _photoAppService.GetPhotoAsync(channel.PhotoId);
                    return _layeredChatService.GetConverter(input.Layer).ToChatFull(
                        input.UserId,
                        channel,
                        photoReadModel,
                        channelFull!,
                        channelMember,
                        peerNotifySettings,
                        migratedFromChatReadModel
                        );
                }
            case PeerType.Chat:
                {
                    var chat = await _queryProcessor
                        .ProcessAsync(new GetChatByChatIdQuery(obj.ChatId), CancellationToken.None)
                 ;

                    if (chat == null)
                    {
                        //ThrowHelper.ThrowUserFriendlyException("CHAT_ID_INVALID");
                        RpcErrors.RpcErrors400.ChatIdInvalid.ThrowRpcError();
                    }

                    var migrateToChannelReadModel = chat!.MigrateToChannelId.HasValue
                        ? await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(chat.MigrateToChannelId.Value),default)
                        : null;

                    var userList = chat!.IsDeleted ? Array.Empty<IUserReadModel>() : await _queryProcessor
                        .ProcessAsync(new GetUsersByUidListQuery(chat!.ChatMembers.Select(p => p.UserId).ToList()),
                            CancellationToken.None);
                    var contactReadModels = chat.IsDeleted ? Array.Empty<IContactReadModel>() : await _queryProcessor
                        .ProcessAsync(new GetContactListQuery(input.UserId, userList.Select(p => p.UserId).ToList()), default)
                 ;

                    var peerNotifySettings = await _queryProcessor
                        .ProcessAsync(
                            new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                                PeerType.Chat,
                                obj.ChatId)),
                            CancellationToken.None);

                    var privacyList = await _privacyAppService.GetPrivacyListAsync(userList.Select(p => p.UserId).ToList());

                    var photoIds = new List<long>();
                    photoIds.AddRange(userList.Select(p => p.ProfilePhotoId ?? 0));
                    photoIds.AddRange(userList.Select(p => p.FallbackPhotoId ?? 0));
                    photoIds.AddRange(contactReadModels.Select(p => p.PhotoId ?? 0));
                    photoIds.Add(chat.PhotoId ?? 0);
                    photoIds.Add(migrateToChannelReadModel?.PhotoId ?? 0);
                    photoIds.RemoveAll(p => p == 0);

                    var photos = await _photoAppService.GetPhotosAsync(photoIds);

                    var users = _layeredUserService.GetConverter(input.Layer)
                        .ToUserList(input.UserId, userList, photos, contactReadModels, privacyList);

                    //var photoReadModel = await _photoAppService.GetPhotoAsync(chat.PhotoId);
                    var photoReadModel = chat.MigrateToChannelId.HasValue ?
                 photos.FirstOrDefault(p => p.PhotoId == migrateToChannelReadModel?.PhotoId) :
                        photos.FirstOrDefault(p => p.PhotoId == chat.PhotoId);

                    return _layeredChatService.GetConverter(input.Layer).ToChatFull(
                        input.UserId,
                        chat,
                        photoReadModel,
                        users,
                        peerNotifySettings,
                        migrateToChannelReadModel
                        );
                }
        }

        throw new NotImplementedException($"Not supported peer type {peerType},chatId={obj.ChatId}");
    }
}
