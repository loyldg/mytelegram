namespace MyTelegram.ReadModel.ReadModelLocators;

public class ChatInviteReadModelLocator : IChatInviteReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        switch (aggregateEvent)
        {
            case ChannelInviteExportedEvent exportChatInviteEvent:
                yield return ChatInviteId.Create(exportChatInviteEvent.ChannelId, exportChatInviteEvent.InviteId).Value;
                break;
            case ChannelInviteEditedEvent exportChatInviteEditedEvent:
                yield return ChatInviteId.Create(exportChatInviteEditedEvent.ChannelId, exportChatInviteEditedEvent.InviteId).Value;
                break;

            case ChannelInviteDeletedEvent channelInviteDeletedEvent:
                yield return ChatInviteId.Create(channelInviteDeletedEvent.ChannelId, channelInviteDeletedEvent.InviteId).Value;
                break;
        }
    }
}