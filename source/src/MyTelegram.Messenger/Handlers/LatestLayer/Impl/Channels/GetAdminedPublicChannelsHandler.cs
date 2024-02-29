// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/channel">channels/supergroups/geogroups</a> we're admin in. Usually called when the user exceeds the <a href="https://corefork.telegram.org/constructor/config">limit</a> for owned public <a href="https://corefork.telegram.org/api/channel">channels/supergroups/geogroups</a>, and the user is given the choice to remove one of his channels/supergroups/geogroups.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_ADMIN_LOCATED_TOO_MUCH The user has reached the limit of public geogroups.
/// 400 CHANNELS_ADMIN_PUBLIC_TOO_MUCH You're admin of too many public channels, make some channels private to change the username of this channel.
/// See <a href="https://corefork.telegram.org/method/channels.getAdminedPublicChannels" />
///</summary>
internal sealed class GetAdminedPublicChannelsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetAdminedPublicChannels, MyTelegram.Schema.Messages.IChats>,
    Channels.IGetAdminedPublicChannelsHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredChatService;
    private readonly IPhotoAppService _photoAppService;
    public GetAdminedPublicChannelsHandler(IQueryProcessor queryProcessor, ILayeredService<IChatConverter> layeredChatService, IPhotoAppService photoAppService)
    {
        _queryProcessor = queryProcessor;
        _layeredChatService = layeredChatService;
        _photoAppService = photoAppService;
    }

    protected override async Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetAdminedPublicChannels obj)
    {
        var channelIds = (await _queryProcessor.ProcessAsync(new GetAdminedChannelIdsQuery(input.UserId))).ToList();
        var channelReadModels = await _queryProcessor.ProcessAsync(new GetChannelByChannelIdListQuery(channelIds));
        var photoIds = channelReadModels.Select(p => p.PhotoId ?? 0).Distinct().ToList();
        var photoReadModels = await _photoAppService.GetPhotosAsync(channelReadModels);
        var channelMemberReadModels =
            await _queryProcessor.ProcessAsync(new GetChannelMemberListByChannelIdListQuery(input.UserId, channelIds));
        var chats = _layeredChatService.GetConverter(input.Layer).ToChannelList(input.UserId, channelReadModels, photoReadModels, channelIds,
            channelMemberReadModels);

        return new TChats
        {
            Chats = new TVector<IChat>(chats)
        };
    }
}
