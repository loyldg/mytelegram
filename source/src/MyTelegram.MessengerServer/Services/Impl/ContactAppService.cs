namespace MyTelegram.MessengerServer.Services.Impl;

public class ContactAppService : BaseAppService, IContactAppService
{
    private readonly IQueryProcessor _queryProcessor;

    public ContactAppService(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    public async Task<SearchContactOutput> SearchAsync(long selfUserId,
        string? keyword)
    {
        if (keyword?.Length > 0)
        {
            var searchKey = keyword;
            if (searchKey.StartsWith("@"))
            {
                searchKey = keyword[1..];
            }

            var userList =
                await _queryProcessor.ProcessAsync(new SearchUserByKeywordQuery(keyword, 20),
                    CancellationToken.None).ConfigureAwait(false);

            //var contactReadModels = await _queryProcessor
            //    .ProcessAsync(new GetContactListQuery(selfUserId, userList.Select(p => p.UserId).ToList()), default)
            //    .ConfigureAwait(false);

            //var contactReadModels = await _queryProcessor
            //    .ProcessAsync(new SearchContactQuery(selfUserId, searchKey), default).ConfigureAwait(false);

            var userNameReadModel = await _queryProcessor
                .ProcessAsync(new SearchUserNameQuery(searchKey), default).ConfigureAwait(false);

            var channelIdList = userNameReadModel.Where(p => p.PeerType == PeerType.Channel).Select(p => p.PeerId)
                .ToList();
            var userIdList = userNameReadModel.Where(p => p.PeerType == PeerType.User).Select(p => p.PeerId).ToList();

            var userList2 = await _queryProcessor
                .ProcessAsync(new GetUsersByUidListQuery(userIdList), default).ConfigureAwait(false);
            var allUserList = userList.ToList();
            allUserList.AddRange(userList2);

            var channelList = await _queryProcessor
                .ProcessAsync(new GetChannelByChannelIdListQuery(channelIdList), default).ConfigureAwait(false);

            var myChannelIdList = await _queryProcessor
                .ProcessAsync(new GetChannelIdListByMemberUidQuery(selfUserId), default).ConfigureAwait(false);
            var myChannelList = channelList.Where(p => myChannelIdList.Contains(p.ChannelId)).ToList();
            var otherChannelList = channelList.Where(p => !myChannelIdList.Contains(p.ChannelId)).ToList();

            //var privacyList = await _queryProcessor
            //    .ProcessAsync(new GetPrivacyListQuery(userIdList,
            //            new[] { PrivacyType.PhoneNumber, PrivacyType.StatusTimestamp }),
            //        default).ConfigureAwait(false);
            IReadOnlyCollection<IChannelMemberReadModel> channelMemberList = new List<IChannelMemberReadModel>();
            if (channelIdList.Count > 0)
            {
                channelMemberList = await _queryProcessor
                    .ProcessAsync(new GetChannelMemberListByChannelIdListQuery(selfUserId, channelIdList),
                        default).ConfigureAwait(false);
            }

            return new SearchContactOutput(selfUserId,
                allUserList,
                //contactReadModels,
                myChannelList,
                otherChannelList,
                //privacyList,
                channelMemberList);
        }

        return new SearchContactOutput(selfUserId,
            new List<IUserReadModel>(),
            //new List<IContactReadModel>(),
            new List<IChannelReadModel>(),
            new List<IChannelReadModel>(),
            //new List<IPrivacyReadModel>(),
            new List<IChannelMemberReadModel>());
    }
}