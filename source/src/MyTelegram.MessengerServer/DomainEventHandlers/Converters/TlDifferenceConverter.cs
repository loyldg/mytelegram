using MyTelegram.Schema.Updates;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public class TlDifferenceConverter : ITlDifferenceConverter
{
    private readonly ITlMessageConverter _messageConverter;
    private readonly ITlChatConverter _chatConverter;
    private readonly ITlUserConverter _userConverter;
    private readonly IObjectMapper _objectMapper;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;
    public TlDifferenceConverter(ITlMessageConverter messageConverter,
        ITlChatConverter chatConverter,
        ITlUserConverter userConverter,
        IObjectMapper objectMapper,
        IOptions<MyTelegramMessengerServerOptions> options)
    {
        _messageConverter = messageConverter;
        _chatConverter = chatConverter;
        _userConverter = userConverter;
        _objectMapper = objectMapper;
        _options = options;
    }

    public IChannelDifference ToChannelDifference(GetMessageOutput output,
        bool isChannelMember,
        IList<IUpdate> updatesList,
        int updatesMaxPts = 0,
        bool resetLeftToFalse = false)
    {
        var timeout = _options.Value.ChannelGetDifferenceIntervalSeconds;
        if (output.MessageList.Count == 0 && updatesList.Count == 0)
        {
            return new TChannelDifferenceEmpty { Final = true, Pts = output.Pts, Timeout = timeout };
        }

        var maxPts = updatesMaxPts;
        if (output.MessageList.Count > 0)
        {
            var boxMaxPts = output.MessageList.Max(p => p.Pts);
            maxPts = Math.Max(updatesMaxPts, boxMaxPts);
        }

        var messageList = _messageConverter.ToMessages(output.MessageList, output.SelfUserId);
        var chatList = _chatConverter.ToChatList(output.ChatList, output.SelfUserId);
        var channelList = _chatConverter.ToChannelList(output.ChannelList,
            output.JoinedChannelIdList,
            output.ChannelMemberList,
            output.SelfUserId,
            resetLeftToFalse
        );
        var userList = _userConverter.ToUserList(output.UserList, output.SelfUserId);
        chatList.AddRange(channelList);
        return new TChannelDifference
        {
            Final = output.Pts == maxPts,
            Pts = maxPts,
            Users = new TVector<IUser>(userList),
            OtherUpdates = new TVector<IUpdate>(updatesList),
            Timeout = timeout,
            Chats = new TVector<IChat>(chatList),
            NewMessages = new TVector<IMessage>(messageList)
        };
    }

    public IDifference ToDifference(GetMessageOutput output,
        IPtsReadModel? pts,
        int cachedPts,
        int limit,
        IList<IUpdate> updateList,
        IList<IChat> chatListFromUpdates)
    {
        var messageList = _messageConverter.ToMessages(output.MessageList, output.SelfUserId);
        var userList = _userConverter.ToUserList(output.UserList, output.SelfUserId);
        var chatList = _chatConverter.ToChatList(output.ChatList, output.SelfUserId);
        chatList.AddRange(chatListFromUpdates);
        var channelList = _chatConverter.ToChannelList(output.ChannelList,
            output.JoinedChannelIdList,
            output.ChannelMemberList,
            output.SelfUserId,
            true);
        chatList.AddRange(channelList);

        if (updateList.Count == limit)
        {
            var differenceSlice = new TDifferenceSlice
            {
                Chats = new TVector<IChat>(chatList),
                NewEncryptedMessages = new TVector<IEncryptedMessage>(),
                NewMessages = new TVector<IMessage>(messageList),
                OtherUpdates = new TVector<IUpdate>(updateList),
                Users = new TVector<IUser>(userList),
                IntermediateState = pts == null ? new TState
                {
                    Date = DateTime.UtcNow.ToTimestamp()
                } : _objectMapper.Map<IPtsReadModel, TState>(pts)
            };

            return differenceSlice;
        }

        var difference = new TDifference
        {
            Chats = new TVector<IChat>(chatList),
            NewEncryptedMessages = new TVector<IEncryptedMessage>(),
            NewMessages = new TVector<IMessage>(messageList),
            OtherUpdates = new TVector<IUpdate>(updateList),
            Users = new TVector<IUser>(userList),
            State = pts == null ? new TState
            {
                Date = DateTime.UtcNow.ToTimestamp()
            } : _objectMapper.Map<IPtsReadModel, TState>(pts)
        };
        if (cachedPts > pts?.Pts)
        {
            difference.State.Pts = cachedPts;
        }

        return difference;
    }
}