namespace MyTelegram.MessengerServer.Services;

public class GetDialogOutput
{
    public GetDialogOutput(
        long selfUserId,
        IReadOnlyCollection<IDialogReadModel> dialogList,
        IReadOnlyCollection<IMessageReadModel> messageList,
        IReadOnlyCollection<IUserReadModel> userList,
        IReadOnlyCollection<IChatReadModel> chatList,
        IReadOnlyCollection<IChannelReadModel> channelList,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberList,
        int limit
        //,
        //PtsReadModel ptsReadModel
        //IReadOnlyCollection<PeerNotifySettingsReadModel> peerNotifySettingList
    )
    {
        SelfUserId = selfUserId;
        DialogList = dialogList;
        MessageList = messageList;
        UserList = userList;
        ChatList = chatList;
        ChannelList = channelList;
        ChannelMemberList = channelMemberList;
        Limit = limit;
        //PtsReadModel = ptsReadModel;
        //PeerNotifySettingList = peerNotifySettingList;
    }

    public int CachedPts { get; set; }
    public IReadOnlyCollection<IChannelReadModel> ChannelList { get; set; }
    public IReadOnlyCollection<IChannelMemberReadModel> ChannelMemberList { get; set; }
    public int Limit { get; }
    public IReadOnlyCollection<IChatReadModel> ChatList { get; set; }


    public IReadOnlyCollection<IDialogReadModel> DialogList { get; set; }
    public IReadOnlyCollection<IMessageReadModel> MessageList { get; set; }
    public IPtsReadModel? PtsReadModel { get; set; }

    public long SelfUserId { get; set; }

    public IReadOnlyCollection<IUserReadModel> UserList { get; set; }
    //public IReadOnlyCollection<PeerNotifySettingsReadModel> PeerNotifySettingList { get; }
}