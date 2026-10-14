namespace MyTelegram.Messenger.Converters.ConverterServices.Channels;

internal sealed class JoinChannelConverterService(IMessageConverterService messageConverterService) : IJoinChannelConverterService, ITransientDependency
{
    public MyTelegram.Schema.IUpdates ToJoinChannelUpdates(SendOutboxMessageCompletedSagaEvent data)
    {
        var item = data.MessageItem;
        var channelId = item.ToPeer.PeerId;
        var updateMessageId = new TUpdateMessageID
        {
            Id = item.MessageId,
            RandomId = item.RandomId
        };

        var updateChannel = new TUpdateChannel
        {
            ChannelId = channelId
        };

        var updateReadChannelInbox = new TUpdateReadChannelInbox
        {
            ChannelId = channelId,
            MaxId = item.MessageId,
            Pts = item.Pts
        };

        var updateNewChannelMessage = new TUpdateNewChannelMessage
        {
            Message = messageConverterService.ToMessage(data.RequestInfo.UserId, item),
            Pts = item.Pts,
            PtsCount = 1
        };

        var updates = new TUpdates
        {
            Updates = [updateMessageId, updateChannel, updateReadChannelInbox, updateNewChannelMessage],
            Chats = [],
            Date = item.Date,
            Users = []
        };

        return updates;
    }

    public MyTelegram.Schema.IUpdates ToJoinChannelUpdates(long selfUserId, SendOutboxMessageCompletedSagaEvent data)
    {
        var updateNewChannelMessage = new TUpdateNewChannelMessage
        {
            Message = messageConverterService.ToMessage(selfUserId, data.MessageItem),
            Pts = data.MessageItem.Pts,
            PtsCount = 1
        };

        return new TUpdates
        {
            Date = data.MessageItem.Date,
            Chats = [],
            Users = [],
            Updates = [updateNewChannelMessage]
        };
    }
}
