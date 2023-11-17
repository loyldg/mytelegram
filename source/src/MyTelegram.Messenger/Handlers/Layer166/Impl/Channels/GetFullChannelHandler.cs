// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get full info about a <a href="https://corefork.telegram.org/api/channel#supergroups">supergroup</a>, <a href="https://corefork.telegram.org/api/channel#gigagroups">gigagroup</a> or <a href="https://corefork.telegram.org/api/channel#channels">channel</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHANNEL_PUBLIC_GROUP_NA channel/supergroup not available.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.getFullChannel" />
///</summary>
internal sealed class GetFullChannelHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetFullChannel, MyTelegram.Schema.Messages.IChatFull>,
    Channels.IGetFullChannelHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredService;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPhotoAppService _photoAppService;
    private readonly ILogger<GetFullChannelHandler> _logger;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;

    public GetFullChannelHandler(IQueryProcessor queryProcessor,
        ILayeredService<IChatConverter> layeredService,
        IAccessHashHelper accessHashHelper,
        IPhotoAppService photoAppService, ILogger<GetFullChannelHandler> logger, IOptions<MyTelegramMessengerServerOptions> options)
    {
        _queryProcessor = queryProcessor;
        _layeredService = layeredService;
        _accessHashHelper = accessHashHelper;
        _photoAppService = photoAppService;
        _logger = logger;
        _options = options;
    }

    protected override async Task<MyTelegram.Schema.Messages.IChatFull> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetFullChannel obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            var channel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId), default);
            if (channel == null)
            {
                RpcErrors.RpcErrors400.ChannelInvalid.ThrowRpcError();
            }

            var channelFull = await _queryProcessor.ProcessAsync(new GetChannelFullByIdQuery(inputChannel.ChannelId), default);
            var dialogReadModel = await _queryProcessor.ProcessAsync(
                new GetDialogByIdQuery(DialogId.Create(input.UserId, PeerType.Channel, inputChannel.ChannelId)), default);
            if (dialogReadModel == null)
            {
                _logger.LogWarning("Dialog not exists,userId={UserId},ToPeer={ToPeer}", input.UserId, new Peer(PeerType.Channel, inputChannel.ChannelId));
            }
            else
            {
                channelFull!.ReadInboxMaxId = dialogReadModel.ReadInboxMaxId;
                channelFull.ReadOutboxMaxId = dialogReadModel.ReadOutboxMaxId;
                //channelFull.UnreadCount = dialogReadModel.UnreadCount;
                var maxId = new[]{dialogReadModel.ReadInboxMaxId, dialogReadModel.ReadOutboxMaxId,
                    dialogReadModel.ChannelHistoryMinId}.Max();
                channelFull.UnreadCount = channel!.TopMessageId - maxId;
            }

            var channelMember = await _queryProcessor
                .ProcessAsync(new GetChannelMemberByUidQuery(inputChannel.ChannelId, input.UserId), default);
            var migratedFromChatReadModel = channelFull!.MigratedFromChatId == null ? null :
                await _queryProcessor.ProcessAsync(new GetChatByChatIdQuery(channelFull.MigratedFromChatId.Value),default);

            var peerNotifySettings = await _queryProcessor
                .ProcessAsync(
                    new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                        PeerType.Channel,
                        inputChannel.ChannelId)), default);
            //var chatPhoto = _layeredPhotoService.GetConverter(input.Layer).ToChatPhoto(channel!.Photo);
            var photoReadModel = await _photoAppService.GetPhotoAsync(channel!.PhotoId);
            IChatInviteReadModel? chatInviteReadModel = null;
            if (channel.AdminList.Any(p => p.UserId == input.UserId))
            {
                chatInviteReadModel =
                    await _queryProcessor.ProcessAsync(new GetPermanentChatInviteQuery(inputChannel.ChannelId), default);
                if (chatInviteReadModel != null)
                {
                    chatInviteReadModel.Link = $"{_options.Value.JoinChatDomain}/+{chatInviteReadModel.Link}";
                }
            }

            var r = _layeredService.GetConverter(input.Layer).ToChatFull(
                input.UserId,
                channel!,
                photoReadModel,
                channelFull!,
                channelMember,
                peerNotifySettings,
                migratedFromChatReadModel,
                chatInviteReadModel
                );

            return r;
        }

        throw new NotImplementedException();
    }
}
