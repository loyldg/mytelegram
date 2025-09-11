namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Globally search for posts from public <a href="https://corefork.telegram.org/api/channel">channels »</a> (<em>including</em> those we aren't a member of) containing a specific hashtag.
/// See <a href="https://corefork.telegram.org/method/channels.searchPosts" />
///</summary>
internal sealed class SearchPostsHandler(
    IQueryProcessor queryProcessor,
    IChatConverterService chatConverterService,
    IUserConverterService userConverterService,
    IMessageConverterService messageConverterService,
    IPeerHelper peerHelper, IMessageAppService messageAppService) : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestSearchPosts, MyTelegram.Schema.Messages.IMessages>
{
    protected override async Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestSearchPosts obj)
    {
        if (string.IsNullOrEmpty(obj.Hashtag))
        {
            RpcErrors.RpcErrors400.HashtagInvalid.ThrowRpcError();
        }

        var peer = peerHelper.GetPeer(obj.OffsetPeer);
        var messageReadModels = await queryProcessor.ProcessAsync(new SearchPostsQuery(obj.Hashtag, obj.OffsetRate, peer.PeerId, obj.OffsetId, obj.Limit));
        var messages = messageConverterService.ToMessageList(input.UserId, messageReadModels, [], [], [], input.Layer);
        var (userIds, channelIds) = messageAppService.GetExtraPeerIds(messageReadModels);
        var channelIdList = channelIds.ToList();
        var channelMemberReadModels =
            await queryProcessor.ProcessAsync(
                new GetChannelMemberListByChannelIdListQuery(input.UserId, channelIdList));
        var channels =
            await chatConverterService.GetChannelListAsync(input, channelIdList, channelMemberReadModels,
                input.Layer);
        var users = await userConverterService.GetUserListAsync(input, userIds.ToList(), false, false,
            input.Layer);

        if (messageReadModels.Count == obj.Limit && messageReadModels.Count > 0)
        {
            var nextRate = messageReadModels.Max(p => p.Date);
            var totalCount = await queryProcessor.ProcessAsync(new GetPostsCountQuery(obj.Hashtag, obj.OffsetRate, obj.OffsetId));

            return new TMessagesSlice
            {
                Count = totalCount,
                Chats = [.. channels],
                Messages = [.. messages],
                Users = [.. users],
                NextRate = nextRate,
            };
        }

        return new TMessages
        {
            Chats = [.. channels],
            Messages = [.. messages],
            Users = [.. users],
        };
    }
}
