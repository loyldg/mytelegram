namespace MyTelegram.Messenger.Services;

public class GetMessageOutput(
    IReadOnlyCollection<IChannelReadModel> channelList,
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
    IReadOnlyCollection<IUserReactionReadModel> userReactionList,
    bool hasMoreData,
    bool isSearchGlobal,
    int pts,
    long selfUserId,
    int limit,
    OffsetInfo? offsetInfo
    )
{
    //    public void SetChannelReadModelList(IReadOnlyCollection<IChannelReadModel> channelReadModels)
    //{
    //    ChannelList = channelReadModels;
    //}

    public GetMessageOutput() : this(
        [],
        [],
        [],
        [],
        [],
        [],
        [],
        [],
        [],
        [],
        [],
        [],
        false, false, 0, 0, 0,
        null
        )
    {
    }

    public IReadOnlyCollection<IChannelReadModel> ChannelList { get; internal set; } = channelList;
    public IReadOnlyCollection<IPhotoReadModel> PhotoList { get; internal set; } = photoList;
    public IReadOnlyCollection<IChannelMemberReadModel> ChannelMemberList { get; init; } = channelMemberList;
    public IReadOnlyCollection<IChatReadModel> ChatList { get; init; } = chatList;
    public IReadOnlyCollection<IContactReadModel> ContactList { get; init; } = contactList;
    public IReadOnlyCollection<long> JoinedChannelIdList { get; init; } = joinedChannelIdList;
    public IReadOnlyCollection<IMessageReadModel> MessageList { get; set; } = messageList;
    public IReadOnlyCollection<IPrivacyReadModel> PrivacyList { get; init; } = privacyList;
    public IReadOnlyCollection<IUserReadModel> UserList { get; init; } = userList;
    public IReadOnlyCollection<IPollReadModel>? PollList { get; init; } = pollList;
    public IReadOnlyCollection<IPollAnswerVoterReadModel>? ChosenPollOptions { get; init; } = chosenPollOptions;
    public IReadOnlyCollection<IUserReactionReadModel> UserReactionList { get; } = userReactionList;
    public bool HasMoreData { get; init; } = hasMoreData;
    public bool IsSearchGlobal { get; init; } = isSearchGlobal;
    public int Pts { get; init; } = pts;
    public long SelfUserId { get; set; } = selfUserId;
    public int Limit { get; set; } = limit;
    public OffsetInfo? OffsetInfo { get; } = offsetInfo;

    /// <summary>
    /// Total number of messages matching the request (messages.messagesSlice/channelMessages count), null if unknown
    /// </summary>
    public int? TotalCount { get; set; }

    /// <summary>
    /// Number of matching messages newer than the first returned message, null if unknown
    /// </summary>
    public int? OffsetIdOffset { get; init; }
}