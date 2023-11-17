// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get all groups that can be used as <a href="https://corefork.telegram.org/api/discussion">discussion groups</a>.Returned <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group chats</a> must be first upgraded to <a href="https://corefork.telegram.org/api/channel#supergroups">supergroups</a> before they can be set as a discussion group.<br>
/// To set a returned supergroup as a discussion group, access to its old messages must be enabled using <a href="https://corefork.telegram.org/method/channels.togglePreHistoryHidden">channels.togglePreHistoryHidden</a>, first.
/// See <a href="https://corefork.telegram.org/method/channels.getGroupsForDiscussion" />
///</summary>
internal sealed class GetGroupsForDiscussionHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetGroupsForDiscussion, MyTelegram.Schema.Messages.IChats>,
    Channels.IGetGroupsForDiscussionHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredService;
    private readonly ILayeredService<IPhotoConverter> _layeredPhotoService;
    private readonly IPhotoAppService _photoAppService;
    public GetGroupsForDiscussionHandler(IQueryProcessor queryProcessor,
        ILayeredService<IChatConverter> layeredService,
        ILayeredService<IPhotoConverter> layeredPhotoService, IPhotoAppService photoAppService)
    {
        _queryProcessor = queryProcessor;
        _layeredService = layeredService;
        _layeredPhotoService = layeredPhotoService;
        _photoAppService = photoAppService;
    }

    protected override async Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetGroupsForDiscussion obj)
    {
        var channelReadModels = await _queryProcessor.ProcessAsync(new GetMegaGroupByUidQuery(input.UserId));
        var photoReadModels = await _photoAppService.GetPhotosAsync(channelReadModels);

        var channelList = _layeredService.GetConverter(input.Layer).ToChannelList(
            input.UserId,
            channelReadModels,
            photoReadModels,
            channelReadModels.Select(p => p.ChannelId).ToList(),
            Array.Empty<IChannelMemberReadModel>(),
            true);

        return new TChats { Chats = new TVector<IChat>(channelList) };
    }
}
