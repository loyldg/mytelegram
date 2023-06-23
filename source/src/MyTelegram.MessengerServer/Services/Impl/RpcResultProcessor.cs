using MyTelegram.Schema.Contacts;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Services.Impl;

public class RpcResultProcessor : IRpcResultProcessor
{
    private readonly ITlChatConverter _chatConverter;
    private readonly ITlMessageConverter _messageConverter;
    private readonly ITlUserConverter _userConverter;

    public RpcResultProcessor(ITlMessageConverter messageConverter,
        ITlChatConverter chatConverter,
        ITlUserConverter userConverter)
    {
        _messageConverter = messageConverter;
        _chatConverter = chatConverter;
        _userConverter = userConverter;
    }

    public IFound ToFound(SearchContactOutput output)
    {
        var userList = _userConverter.ToUserList(output.UserList, output.SelfUserId);
        var peerList = output.UserList.Select(p => (IPeer)new TPeerUser { UserId = p.UserId }).ToList();
        peerList.AddRange(output.MyChannelList.Select(p => (IPeer)new TPeerChannel { ChannelId = p.ChannelId }));
        var otherPeerList = output.ChannelList.Select(p => (IPeer)new TPeerChannel { ChannelId = p.ChannelId });
        //var chatList = ToChannelList(output.ChannelList, output.SelfUserId);
        var myChannelList = _chatConverter.ToChannelList(output.MyChannelList,
            output.MyChannelList.Select(p => p.ChannelId).ToList(),
            output.ChannelMemberList,
            output.SelfUserId);
        var otherChannelList = _chatConverter.ToChannelList(output.ChannelList,
            new List<long>(),
            new List<IChannelMemberReadModel>(),
            output.SelfUserId);
        myChannelList.AddRange(otherChannelList);
        return new TFound
        {
            Chats = new TVector<IChat>(myChannelList),
            MyResults = new TVector<IPeer>(peerList),
            Results = new TVector<IPeer>(otherPeerList),
            Users = new TVector<IUser>(userList)
        };
    }

    public IMessages ToMessages(GetMessageOutput output)
    {
        var messageList = _messageConverter.ToMessages(output.MessageList,
            output.PollList,
            output.ChosenPollOptions,
            output.SelfUserId);
        var userList = _userConverter.ToUserList(output.UserList, output.SelfUserId);
        var chatList = _chatConverter.ToChatList(output.ChatList, output.SelfUserId);
        var channelList = _chatConverter.ToChannelList(output.ChannelList,
            output.JoinedChannelIdList,
            output.ChannelMemberList,
            output.SelfUserId);
        chatList.AddRange(channelList);

        if (output.MessageList.All(p => p.ToPeerType == PeerType.Channel) && !output.IsSearchGlobal)
        {
            var offsetId = messageList.Any() ? messageList.Max(p => p.Id) : 0;
            //var messageIdList=messageList.Select()
            //var offsetId = output.HasMoreData && messageList.Any() ? messageList.Min(p => p.Id) : 0;
            //if(messageList.Count==output.l)
            // Console.WriteLine($"offsetId={offsetId}");
            var channelPts = output.ChannelList.FirstOrDefault()?.Pts ?? output.Pts;
            //var channelPts = output.MessageList.Any() ? output.MessageList.Min(p => p.Pts) : output.Pts;

            return new TChannelMessages
            {
                Chats = new TVector<IChat>(chatList),
                Messages = new TVector<IMessage>(messageList),
                Users = new TVector<IUser>(userList),
                Pts = channelPts,
                Count = messageList.Count,
                OffsetIdOffset = offsetId,
                Topics = new TVector<IForumTopic>()
            };
        }

        return new TMessages
        {
            Chats = new TVector<IChat>(chatList),
            Messages = new TVector<IMessage>(messageList),
            Users = new TVector<IUser>(userList)
        };
    }
}