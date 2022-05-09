namespace MyTelegram.MessengerServer.Services;

public class SearchContactOutput
{
    public SearchContactOutput(long selfUserId,
        IReadOnlyCollection<IUserReadModel> userList,
        //IReadOnlyCollection<IContactReadModel> contactList,
        IReadOnlyCollection<IChannelReadModel> myChannelList,
        IReadOnlyCollection<IChannelReadModel> channelList,
        //IReadOnlyCollection<IPrivacyReadModel> privacyList,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberList
    )
    {
        SelfUserId = selfUserId;
        UserList = userList;
        //ContactList = contactList;
        MyChannelList = myChannelList;
        ChannelList = channelList;
        //PrivacyList = privacyList;
        ChannelMemberList = channelMemberList;
    }

    public IReadOnlyCollection<IChannelReadModel> ChannelList { get; }
    public IReadOnlyCollection<IChannelMemberReadModel> ChannelMemberList { get; }
    //public IReadOnlyCollection<IContactReadModel> ContactList { get; }
    public IReadOnlyCollection<IChannelReadModel> MyChannelList { get; }
    //public IReadOnlyCollection<IPrivacyReadModel> PrivacyList { get; }

    public long SelfUserId { get; }
    public IReadOnlyCollection<IUserReadModel> UserList { get; }
}