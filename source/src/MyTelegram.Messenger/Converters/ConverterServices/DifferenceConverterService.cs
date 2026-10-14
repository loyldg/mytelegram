using MyTelegram.Schema.Updates;

namespace MyTelegram.Messenger.Converters.ConverterServices;

public class DifferenceConverterService(
    IObjectMapper objectMapper,
    IChatConverterService chatConverterService,
    IUserConverterService userConverterService,
    IMessageConverterService messageConverterService) : IDifferenceConverterService, ITransientDependency
{
    public async Task<IChannelDifference> ToChannelDifferenceAsync(IRequestWithAccessHashKeyId request,
        GetMessageOutput output, bool isChannelMember, IList<IUpdate> updatesList,
        int updatesMaxPts = 0,
        bool resetLeftToFalse = false,
        int timeoutSeconds = 30)
    {
        var timeout = timeoutSeconds;
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

        var messageList = messageConverterService.ToMessageList(output.SelfUserId, output.MessageList, output.PollList, output.ChosenPollOptions, output.UserReactionList);

        var channelList = chatConverterService.ToChannelList(request, output.ChannelList, output.PhotoList,
            output.ChannelMemberList, output.JoinedChannelIdList);
        var userList = await userConverterService.ToUserListAsync(request, output.UserList, output.PhotoList,
            output.ContactList, output.PrivacyList);

        return new TChannelDifference
        {
            Final = output.Pts == maxPts,
            Pts = maxPts,
            Users = [.. userList],
            OtherUpdates = [.. updatesList],
            Timeout = timeout,
            Chats = [.. channelList],
            NewMessages = [.. messageList]
        };
    }

    public async Task<IDifference> ToDifferenceAsync(IRequestWithAccessHashKeyId request,
        GetMessageOutput output, IPtsReadModel? pts, int cachedPts, int limit, IList<IUpdate> updateList,
        IList<IChat> chatListFromUpdates, IReadOnlyCollection<IEncryptedMessageReadModel>? encryptedMessageReadModels)
    {
        var messageList = messageConverterService.ToMessageList(output.SelfUserId, output.MessageList, output.PollList,
            output.ChosenPollOptions, output.UserReactionList);
        var userList = await userConverterService.ToUserListAsync(request, output.UserList, output.PhotoList,
            output.ContactList, output.PrivacyList);
        var channelList = chatConverterService.ToChannelList(request, output.ChannelList, output.PhotoList,
            output.ChannelMemberList, output.JoinedChannelIdList);

        var qts = pts?.Qts ?? 0;
        var unreadCount = pts?.UnreadCount ?? 0;
        if (unreadCount < 0)
        {
            unreadCount = 0;
        }

        if (updateList.Count == limit)
        {
            var differenceSlice = new TDifferenceSlice
            {
                Chats = [.. channelList],
                NewEncryptedMessages = [],
                NewMessages = [.. messageList],
                OtherUpdates = [.. updateList],
                Users = [.. userList],
                IntermediateState = pts == null
                    ? new TState
                    {
                        Date = DateTime.UtcNow.ToTimestamp(),
                        Qts = qts,
                        Pts = pts?.Pts ?? 1,
                        UnreadCount = unreadCount,
                        Seq = 1
                    }
                    : objectMapper.Map<IPtsReadModel, TState>(pts)
            };

            return differenceSlice;
        }

        var newEncryptedMessages = Array.Empty<IEncryptedMessage>();

        var difference = new TDifference
        {
            Chats = [.. channelList],
            NewEncryptedMessages = [.. newEncryptedMessages],
            NewMessages = [.. messageList],
            OtherUpdates = [.. updateList],
            Users = [.. userList],
            State = pts == null
                ? new TState
                {
                    Date = DateTime.UtcNow.ToTimestamp(),
                    Qts = qts,
                    Pts = pts?.Pts ?? 1,
                    UnreadCount = unreadCount,
                    Seq = 1
                }
                : objectMapper.Map<IPtsReadModel, TState>(pts)
        };
        if (cachedPts > pts?.Pts)
        {
            difference.State.Pts = cachedPts;
        }

        return difference;
    }
}