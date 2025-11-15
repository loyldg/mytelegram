namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Search for messages and peers globally
/// Possible errors
/// Code Type Description
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// 400 INPUT_FILTER_INVALID The specified filter is invalid.
/// 400 SEARCH_QUERY_EMPTY The search query is empty.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.searchGlobal"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SearchGlobalHandler(IMessageAppService messageAppService, IQueryProcessor queryProcessor, IGetHistoryConverterService getHistoryConverterService) : RpcResultObjectHandler<RequestSearchGlobal, IMessages>
{
    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input, RequestSearchGlobal obj)
    {
        var userId = input.UserId;
        var allJoinedChannelIdList = await queryProcessor.ProcessAsync(new GetAllJoinedChannelIdListQuery(input.UserId));
        var getMessageOutput = await messageAppService.SearchGlobalAsync(new SearchGlobalInput { OwnerPeerId = userId, SelfUserId = userId, Limit = obj.Limit, Q = obj.Q, FolderId = obj.FolderId, OffsetId = obj.OffsetId, JoinedChannelList = allJoinedChannelIdList.ToList(), BroadcastsOnly = obj.BroadcastsOnly, GroupsOnly = obj.GroupsOnly, UsersOnly = obj.UsersOnly });
        return getHistoryConverterService.ToMessages(input, getMessageOutput, input.Layer);
    }
}