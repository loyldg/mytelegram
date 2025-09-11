using GetChannelMemberListByChannelIdListQuery = MyTelegram.Queries.GetChannelMemberListByChannelIdListQuery;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Get all groups that can be used as <a href="https://corefork.telegram.org/api/discussion">discussion groups</a>.Returned <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group chats</a> must be first upgraded to <a href="https://corefork.telegram.org/api/channel#supergroups">supergroups</a> before they can be set as a discussion group.
/// To set a returned supergroup as a discussion group, access to its old messages must be enabled using <a href="https://corefork.telegram.org/method/channels.togglePreHistoryHidden">channels.togglePreHistoryHidden</a>, first.
/// See <a href="https://corefork.telegram.org/method/channels.getGroupsForDiscussion" />
///</summary>
internal sealed class GetGroupsForDiscussionHandler(
    IQueryProcessor queryProcessor,
    IChatConverterService chatConverterService,
    IPhotoAppService photoAppService)
    : RpcResultObjectHandler<RequestGetGroupsForDiscussion,
            IChats>
{
    protected override async Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetGroupsForDiscussion obj)
    {
        var channelReadModels = await queryProcessor.ProcessAsync(new GetMegaGroupByUserIdQuery(input.UserId));
        var photoReadModels = await photoAppService.GetPhotosAsync(channelReadModels);
        var channelMemberReadModel = await queryProcessor.ProcessAsync(
            new GetChannelMemberListByChannelIdListQuery(input.UserId,
                [.. channelReadModels.Select(p => p.ChannelId)]));

        var channelList = chatConverterService.ToChannelList(
            input,
            channelReadModels,
            photoReadModels,
            channelMemberReadModel,
            layer: input.Layer
            );

        return new TChats { Chats = [.. channelList] };
    }
}
