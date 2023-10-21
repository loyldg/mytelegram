namespace MyTelegram.Messenger.Services.Impl;

public class RpcResultProcessorLayer143 : IRpcResultProcessorLayer143
{
    private readonly ILayeredService<IChatConverter> _layeredChatService;
    private readonly ILayeredService<IMessageConverter> _layeredMessageService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    public RpcResultProcessorLayer143(ILayeredService<IMessageConverter> layeredMessageService,
        ILayeredService<IUserConverter> layeredUserService,
        ILayeredService<IChatConverter> layeredChatService)
    {
        _layeredMessageService = layeredMessageService;
        _layeredUserService = layeredUserService;
        _layeredChatService = layeredChatService;
    }

    public virtual int Layer => Layers.Layer143;

    public int RequestLayer { get; set; }

    public IFound ToFound(SearchContactOutput output, int layer)
    {
        var userList = _layeredUserService.GetConverter(layer).ToUserList(output.SelfUserId, output.UserList, output.PhotoList, output.ContactList, output.PrivacyList);
        var peerList = output.UserList.Select(p => (IPeer)new TPeerUser { UserId = p.UserId }).ToList();
        peerList.AddRange(output.MyChannelList.Select(p => (IPeer)new TPeerChannel { ChannelId = p.ChannelId }));
        var otherPeerList = output.ChannelList.Select(p => (IPeer)new TPeerChannel { ChannelId = p.ChannelId });
        //var chatList = ToChannelList(output.ChannelList, output.SelfUserId);
        var myChannelList = _layeredChatService.GetConverter(layer).ToChannelList(
            output.SelfUserId,
            output.MyChannelList,
            output.PhotoList,
            output.MyChannelList.Select(p => p.ChannelId).ToList(),
            output.ChannelMemberList).ToList();

        var otherChannelList = _layeredChatService.GetConverter(layer).ToChannelList(
            output.SelfUserId,
            output.ChannelList,
            output.PhotoList,
            new List<long>(),
            new List<IChannelMemberReadModel>());
        myChannelList.AddRange(otherChannelList);
        return new TFound
        {
            Chats = new TVector<IChat>(myChannelList),
            MyResults = new TVector<IPeer>(peerList),
            Results = new TVector<IPeer>(otherPeerList),
            Users = new TVector<IUser>(userList)
        };
    }

    public IMessages ToMessages(GetMessageOutput output, int layer)
    {
        var messageList = _layeredMessageService.GetConverter(layer).ToMessages(output.MessageList, output.PollList, output.ChosenPollOptions, output.SelfUserId);
        var userList = _layeredUserService.GetConverter(layer).ToUserList(output.SelfUserId, output.UserList, output.PhotoList, output.ContactList, output.PrivacyList);
        var chatList = _layeredChatService.GetConverter(layer).ToChatList(output.SelfUserId, output.ChatList, output.PhotoList).ToList();
        var channelList = _layeredChatService.GetConverter(layer).ToChannelList(
            output.SelfUserId,
            output.ChannelList,
            output.PhotoList,
            output.JoinedChannelIdList,
            output.ChannelMemberList);
        chatList.AddRange(channelList);

        if (output.MessageList.Count > 0 && output.MessageList.All(p => p.ToPeerType == PeerType.Channel) && !output.IsSearchGlobal)
        {
            var offsetId = messageList.Any() ? messageList.Max(p => p.Id) : 0;
            //var messageIdList=messageList.Select()
            //var offsetId = output.HasMoreData && messageList.Any() ? messageList.Min(p => p.Id) : 0;
            //if(messageList.Count==output.l)
            // Console.WriteLine($"offsetId={offsetId}");
            var channelPts = output.ChannelList.FirstOrDefault()?.Pts ?? output.Pts;
            //var channelPts = output.MessageBoxList.Any() ? output.MessageBoxList.Min(p => p.Pts) : output.Pts;

            //return new TChannelMessages
            //{
            //    Chats = new TVector<IChat>(chatList),
            //    Messages = new TVector<IMessage>(messageList),
            //    Users = new TVector<IUser>(userList),
            //    Pts = channelPts,
            //    Count = messageList.Count,
            //    OffsetIdOffset = offsetId
            //};

            return ToChannelMessages(chatList,
                messageList,
                userList,
                channelPts,
                offsetId);
        }

        if (messageList.Count == output.Limit)
        {
            var maxId = messageList.Any() ? messageList.Max(p => p.Id) : 0;
            return new TMessagesSlice
            {
                Chats = new(chatList),
                Count = messageList.Count,
                Inexact = true,
                NextRate = DateTime.UtcNow.AddSeconds(3).ToTimestamp(),
                Messages = new(messageList),
                Users = new(userList),
                OffsetIdOffset = maxId
            };
        }

        return new TMessages
        {
            Chats = new TVector<IChat>(chatList),
            Messages = new TVector<IMessage>(messageList),
            Users = new TVector<IUser>(userList)
        };
    }

    protected virtual IMessages ToChannelMessages(IEnumerable<IChat> chats, IList<IMessage> messages, IEnumerable<IUser> users, int channelPts, int offsetIdOffset)
    {
        return new TChannelMessages
        {
            Chats = new TVector<IChat>(chats),
            Messages = new TVector<IMessage>(messages),
            Users = new TVector<IUser>(users),
            Pts = channelPts,
            Count = messages.Count,
            OffsetIdOffset = offsetIdOffset
        };
    }
}