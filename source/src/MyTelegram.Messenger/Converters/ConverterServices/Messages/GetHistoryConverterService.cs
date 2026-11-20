namespace MyTelegram.Messenger.Converters.ConverterServices.Messages;

internal sealed class GetHistoryConverterService(IUserConverterService userConverterService, IChatConverterService chatConverterService,
    IMessageConverterService messageConverterService
    ) : IGetHistoryConverterService, ITransientDependency
{
    public async Task<IMessages> ToMessagesAsync(IRequestWithAccessHashKeyId request, GetMessageOutput output)
    {
        var messages = messageConverterService.ToMessageList(output.SelfUserId,
            output.MessageList,
            output.PollList,
            output.ChosenPollOptions,
            output.UserReactionList);

        var users = await userConverterService.ToUserListAsync(request,
            output.UserList,
            output.PhotoList,
            output.ContactList,
            output.PrivacyList);

        var channels = chatConverterService.ToChannelList(request,
            output.ChannelList,
            output.PhotoList,
            output.ChannelMemberList,
            output.JoinedChannelIdList);

        var hasMessages = messages.Count > 0;

        if (hasMessages && output.MessageList.All(p => p.ToPeerType == PeerType.Channel) && !output.IsSearchGlobal)
        {
            var channelPts = output.ChannelList.FirstOrDefault()?.Pts ?? output.Pts;

            return new TChannelMessages
            {
                Chats = [.. channels],
                Messages = [.. messages],
                Users = [.. users],
                Pts = channelPts,
                Count = output.TotalCount ?? messages.Count,
                OffsetIdOffset = output.OffsetIdOffset,
                Topics = [],
                Inexact = false
            };
        }

        var isSlice = output.TotalCount.HasValue
            ? output.TotalCount.Value > messages.Count
            : messages.Count == output.Limit;

        if (isSlice)
        {
            int? nextRate = null;
            if (output.IsSearchGlobal && hasMessages)
            {
                // The client passes it back as offset_rate together with the peer and id of the last message
                nextRate = output.MessageList.Min(p => p.Date);
            }

            return new TMessagesSlice
            {
                Chats = [.. channels],
                Count = output.TotalCount ?? messages.Count,
                Inexact = !output.TotalCount.HasValue,
                NextRate = nextRate,
                Messages = [.. messages],
                Users = [.. users],
                OffsetIdOffset = output.OffsetIdOffset,
                Topics = []
            };
        }

        return new TMessages
        {
            Chats = [.. channels],
            Messages = [.. messages],
            Users = [.. users],
            Topics = []
        };
    }
}
