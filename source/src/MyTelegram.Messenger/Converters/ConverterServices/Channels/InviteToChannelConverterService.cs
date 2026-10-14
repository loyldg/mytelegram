namespace MyTelegram.Messenger.Converters.ConverterServices.Channels;

internal sealed class InviteToChannelConverterService(IMessageConverterService messageConverterService) : IInviteToChannelConverterService, ITransientDependency
{
    public IInvitedUsers ToInvitedUsers(SendOutboxMessageCompletedSagaEvent data)
    {
        var item = data.MessageItem;
        var updateMessageId = new TUpdateMessageID
        {
            Id = item.MessageId,
            RandomId = item.RandomId
        };
        var updateReadChannelInbox = new TUpdateReadChannelInbox
        {
            ChannelId = item.ToPeer.PeerId,
            MaxId = item.MessageId,
            Pts = item.Pts,
            StillUnreadCount = 0
        };

        var updateNewChannelMessage = new TUpdateNewChannelMessage
        {
            Pts = item.Pts,
            PtsCount = 1,
            Message = messageConverterService.ToMessage(0, item)
        };

        return new TInvitedUsers
        {
            Updates = new TUpdates
            {
                Date = item.Date,
                Chats = [],
                Updates = [updateMessageId, updateReadChannelInbox, updateNewChannelMessage],
                Users = []
            },
            MissingInvitees = []
        };
    }

    public IUpdates ToInviteToChannelUpdates(
        SendOutboxMessageCompletedSagaEvent data
        )
    {
        var item = data.MessageItem;
        var updateNewChannelMessage = new TUpdateNewChannelMessage
        {
            Pts = item.Pts,
            PtsCount = 1,
            Message = messageConverterService.ToMessage(-1, item)
        };

        return new TUpdates
        {
            Chats = [],
            Date = item.Date,
            Updates = [updateNewChannelMessage],
            Users = []
        };
    }
}
