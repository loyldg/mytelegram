namespace MyTelegram.Messenger.Services;

public class GetUpdatesOutput
{
    public List<IUpdatesReadModel> UpdatesReadModels { get; set; }
    public GetMessageOutput MessageOutput { get; set; }
}

public class GetMessageOutput
{
    public GetMessageOutput(IReadOnlyCollection<IChannelReadModel> channelList,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberList,
        IReadOnlyCollection<IChatReadModel> chatList,
        IReadOnlyCollection<IContactReadModel> contactList,
        IReadOnlyCollection<long> joinedChannelIdList,
        IReadOnlyCollection<IMessageReadModel> messageList,
        IReadOnlyCollection<IPrivacyReadModel> privacyList,
        IReadOnlyCollection<IUserReadModel> userList,
        IReadOnlyCollection<IPhotoReadModel> photoList,
        IReadOnlyCollection<IPollReadModel>? pollList,
        IReadOnlyCollection<IPollAnswerVoterReadModel>? chosenPollOptions,
        bool hasMoreData,
        bool isSearchGlobal,
        int pts,
        long selfUserId,
        int limit)
    {
        ChannelList = channelList;
        ChannelMemberList = channelMemberList;
        ChatList = chatList;
        ContactList = contactList;
        JoinedChannelIdList = joinedChannelIdList;
        MessageList = messageList;
        PrivacyList = privacyList;
        UserList = userList;
        PhotoList = photoList;
        PollList = pollList;
        ChosenPollOptions = chosenPollOptions;
        HasMoreData = hasMoreData;
        IsSearchGlobal = isSearchGlobal;
        Pts = pts;
        SelfUserId = selfUserId;
        Limit = limit;
    }
    //    public void SetChannelReadModelList(IReadOnlyCollection<IChannelReadModel> channelReadModels)
    //{
    //    ChannelList = channelReadModels;
    //}

    public GetMessageOutput()
    {
        ChannelList = Array.Empty<IChannelReadModel>();
        PhotoList = Array.Empty<IPhotoReadModel>();
        ChannelMemberList = Array.Empty<IChannelMemberReadModel>();
        ChatList = Array.Empty<IChatReadModel>();
        ContactList = Array.Empty<IContactReadModel>();
        JoinedChannelIdList = Array.Empty<long>();
        MessageList = Array.Empty<IMessageReadModel>();
        PrivacyList = Array.Empty<IPrivacyReadModel>();
        UserList = Array.Empty<IUserReadModel>();
        PollList = Array.Empty<IPollReadModel>();
        ChosenPollOptions = Array.Empty<IPollAnswerVoterReadModel>();
    }

    public IReadOnlyCollection<IChannelReadModel> ChannelList { get; internal set; }
    public IReadOnlyCollection<IPhotoReadModel> PhotoList { get; internal set; }
    public IReadOnlyCollection<IChannelMemberReadModel> ChannelMemberList { get; init; }
    public IReadOnlyCollection<IChatReadModel> ChatList { get; init; }
    public IReadOnlyCollection<IContactReadModel> ContactList { get; init; }
    public IReadOnlyCollection<long> JoinedChannelIdList { get; init; }
    public IReadOnlyCollection<IMessageReadModel> MessageList { get; init; }
    public IReadOnlyCollection<IPrivacyReadModel> PrivacyList { get; init; }
    public IReadOnlyCollection<IUserReadModel> UserList { get; init; }
    public IReadOnlyCollection<IPollReadModel>? PollList { get; init; }
    public IReadOnlyCollection<IPollAnswerVoterReadModel>? ChosenPollOptions { get; init; }
    public bool HasMoreData { get; init; }
    public bool IsSearchGlobal { get; init; }
    public int Pts { get; init; }
    public long SelfUserId { get; set; }
    public int Limit { get; set; }

}