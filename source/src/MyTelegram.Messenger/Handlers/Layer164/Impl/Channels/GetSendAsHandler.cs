// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Obtains a list of peers that can be used to send messages in a specific group
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.getSendAs" />
///</summary>
internal sealed class GetSendAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetSendAs, MyTelegram.Schema.Channels.ISendAsPeers>,
    Channels.IGetSendAsHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredChatService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly ILayeredService<ISendAsPeerConverter> _layeredSendAsPeerService;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPhotoAppService _photoAppService;
    public GetSendAsHandler(
        IQueryProcessor queryProcessor,
        ILayeredService<IChatConverter> layeredChatService,
        ILayeredService<IUserConverter> layeredUserService,
        ILayeredService<ISendAsPeerConverter> layeredSendAsPeerService,
        IAccessHashHelper accessHashHelper, IPhotoAppService photoAppService)
    {
        _queryProcessor = queryProcessor;
        _layeredChatService = layeredChatService;
        _layeredUserService = layeredUserService;
        _layeredSendAsPeerService = layeredSendAsPeerService;
        _accessHashHelper = accessHashHelper;
        _photoAppService = photoAppService;
    }

    protected override async Task<MyTelegram.Schema.Channels.ISendAsPeers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetSendAs obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputPeerChannel.ChannelId, inputPeerChannel.AccessHash);

            var channelReadModel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(inputPeerChannel.ChannelId), default)
         ;
            var photoReadModel = await _photoAppService.GetPhotoAsync(channelReadModel.PhotoId);
            // Only channel creator can send as channel peer
            if (channelReadModel.CreatorId == input.UserId)
            {
                var channel = _layeredChatService.GetConverter(input.Layer).ToChannel(
                    input.UserId,
                    channelReadModel,
                    photoReadModel,
                    null, false);
                return _layeredSendAsPeerService.GetConverter(input.Layer).ToSendAsPeers(input.UserId,
                    channelReadModel.ChannelId,
                    channelReadModel.CreatorId,
                    channel,
                    null);
            }
            else
            {
                var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(input.UserId), default)
             ;
                var photos = await _photoAppService.GetPhotosAsync(userReadModel);
                return _layeredSendAsPeerService.GetConverter(input.Layer).ToSendAsPeers(input.UserId,
                    channelReadModel.ChannelId,
                    channelReadModel.CreatorId,
                    null,
                    _layeredUserService.GetConverter(input.Layer).ToUser(input.UserId, userReadModel!, photos));
            }
        }

        throw new NotImplementedException();
    }
}
