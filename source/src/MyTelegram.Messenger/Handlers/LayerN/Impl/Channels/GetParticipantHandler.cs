// ReSharper disable All

namespace MyTelegram.Handlers.Channels.LayerN;

///<summary>
/// Get info about a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> participant
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PARTICIPANT_ID_INVALID The specified participant ID is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/channels.getParticipant" />
///</summary>
internal sealed class GetParticipantHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.LayerN.RequestGetParticipant, MyTelegram.Schema.Channels.IChannelParticipant>,
    Channels.LayerN.IGetParticipantHandler, IProcessedHandler
{
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly ILayeredService<IPhotoConverter> _layeredPhotoService;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPhotoAppService _photoAppService;
    private readonly IPrivacyAppService _privacyAppService;
    public GetParticipantHandler(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper,
        ILayeredService<IChatConverter> layeredService,
        IAccessHashHelper accessHashHelper,
        ILayeredService<IUserConverter> layeredUserService,
        ILayeredService<IPhotoConverter> layeredPhotoService, IPhotoAppService photoAppService, IPrivacyAppService privacyAppService)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _layeredService = layeredService;
        _accessHashHelper = accessHashHelper;
        _layeredUserService = layeredUserService;
        _layeredPhotoService = layeredPhotoService;
        _photoAppService = photoAppService;
        _privacyAppService = privacyAppService;
    }

    protected override async Task<MyTelegram.Schema.Channels.IChannelParticipant> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.LayerN.RequestGetParticipant obj)
    {
        var peer = _peerHelper.GetPeer(obj.UserId, input.UserId);
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            var channelMemberReadModel = await _queryProcessor
                    .ProcessAsync(new GetChannelMemberByUserIdQuery(inputChannel.ChannelId, peer.PeerId), default)
                ;

            if (channelMemberReadModel == null)
            {
                //ThrowHelper.ThrowUserFriendlyException("USER_NOT_PARTICIPANT");
                RpcErrors.RpcErrors400.UserNotParticipant.ThrowRpcError();
            }

            var userReadModel = await _queryProcessor
                    .ProcessAsync(new GetUserByIdQuery(channelMemberReadModel!.UserId), default)
                ;

            if (userReadModel == null)
            {
                //ThrowHelper.ThrowUserFriendlyException("USER_ID_INVALID");
                RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
            }

            var channelReadModel = await _queryProcessor
                .ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId), default);

            var privacies = await _privacyAppService.GetPrivacyListAsync(userReadModel!.UserId);
            var photos = await _photoAppService.GetPhotosAsync(userReadModel);
            var user = _layeredUserService.GetConverter(input.Layer)
                .ToUser(input.UserId, userReadModel, photos, privacies: privacies);
            //var chatPhoto = _layeredPhotoService.GetConverter(input.Layer).GetChatPhoto(channelReadModel.Photo);
            var chatPhoto = await _photoAppService.GetPhotoAsync(channelReadModel.PhotoId);

            return _layeredService.GetConverter(input.Layer).ToChannelParticipantLayerN(
                input.UserId,
                channelReadModel,
                chatPhoto,
                channelMemberReadModel,
                user);
        }

        throw new NotImplementedException();
    }
}
