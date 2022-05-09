namespace MyTelegram.MessengerServer.Services;

public record GetMessageOutput(IReadOnlyCollection<IChannelReadModel> ChannelList,
    IReadOnlyCollection<IChannelMemberReadModel> ChannelMemberList,
    IReadOnlyCollection<IChatReadModel> ChatList,
    IReadOnlyCollection<long> JoinedChannelIdList,
    IReadOnlyCollection<IMessageReadModel> MessageList,
    IReadOnlyCollection<IUserReadModel> UserList,
    bool HasMoreData,
    bool IsSearchGlobal,
    int Pts,
    long SelfUserId)
{
    //    public void SetChannelReadModelList(IReadOnlyCollection<IChannelReadModel> channelReadModels)
    //{
    //    ChannelList = channelReadModels;
    //}

    public IReadOnlyCollection<IChannelReadModel> ChannelList { get; internal set; } = ChannelList;
}