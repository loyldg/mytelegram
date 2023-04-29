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
                    CancellationToken.None);

            //var contactReadModels = await _queryProcessor
            //    .ProcessAsync(new GetContactListQuery(selfUserId, userList.Select(p => p.UserId).ToList()), default)
            //    ;

            //var contactReadModels = await _queryProcessor
            //    .ProcessAsync(new SearchContactQuery(selfUserId, searchKey), default);

            var userNameReadModel = await _queryProcessor
                .ProcessAsync(new SearchUserNameQuery(searchKey), default);

            var channelIdList = userNameReadModel.Where(p => p.PeerType == PeerType.Channel).Select(p => p.PeerId)
                .ToList();
            var userIdList = userNameReadModel.Where(p => p.PeerType == PeerType.User).Select(p => p.PeerId).ToList();

            var userList2 = await _queryProcessor
                .ProcessAsync(new GetUsersByUidListQuery(userIdList), default);
            var allUserList = userList.ToList();
            allUserList.AddRange(userList2);

            var channelList = await _queryProcessor
                .ProcessAsync(new GetChannelByChannelIdListQuery(channelIdList), default);

            var myChannelIdList = await _queryProcessor
                .ProcessAsync(new GetChannelIdListByMemberUidQuery(selfUserId), default);
            var myChannelList = channelList.Where(p => myChannelIdList.Contains(p.ChannelId)).ToList();
            var otherChannelList = channelList.Where(p => !myChannelIdList.Contains(p.ChannelId)).ToList();

            //var privacyList = await _queryProcessor
            //    .ProcessAsync(new GetPrivacyListQuery(userIdList,
            //            new[] { PrivacyType.PhoneNumber, PrivacyType.StatusTimestamp }),
            //        default);
            IReadOnlyCollection<IChannelMemberReadModel> channelMemberList = new List<IChannelMemberReadModel>();
            if (channelIdList.Count > 0)
            {
                channelMemberList = await _queryProcessor
                    .ProcessAsync(new GetChannelMemberListByChannelIdListQuery(selfUserId, channelIdList),
                        default);
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
