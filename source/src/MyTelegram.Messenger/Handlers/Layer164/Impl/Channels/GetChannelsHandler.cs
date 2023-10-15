// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get info about <a href="https://corefork.telegram.org/api/channel">channels/supergroups</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/channels.getChannels" />
///</summary>
internal sealed class GetChannelsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetChannels, MyTelegram.Schema.Messages.IChats>,
    Channels.IGetChannelsHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredService;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPhotoAppService _photoAppService;

    public GetChannelsHandler(IQueryProcessor queryProcessor,
        ILayeredService<IChatConverter> layeredService,
        IAccessHashHelper accessHashHelper, IPhotoAppService photoAppService)
    {
        _queryProcessor = queryProcessor;
        _layeredService = layeredService;
        _accessHashHelper = accessHashHelper;
        _photoAppService = photoAppService;
    }

    protected override async Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetChannels obj)
    {
        var channelIds = new List<long>();
        foreach (var inputChannel in obj.Id)
        {
            if (inputChannel is TInputChannel tInputChannel)
            {
                channelIds.Add(tInputChannel.ChannelId);
                await _accessHashHelper.CheckAccessHashAsync(tInputChannel.ChannelId, tInputChannel.AccessHash);
            }
        }

        if (channelIds.Count > 0)
        {
            var channelReadModels = await _queryProcessor
                .ProcessAsync(new GetChannelByChannelIdListQuery(channelIds), default);
            var photoReadModels = await _photoAppService.GetPhotosAsync(channelReadModels);
            var chats = _layeredService.GetConverter(input.Layer).ToChannelList(
                input.UserId,
                channelReadModels,
                photoReadModels,
                Array.Empty<long>(),
                Array.Empty<IChannelMemberReadModel>());
            return new TChats
            {
                Chats = new TVector<IChat>(chats)
            };
        }

        throw new NotImplementedException();
    }
}
