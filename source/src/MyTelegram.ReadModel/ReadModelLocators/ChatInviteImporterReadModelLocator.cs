namespace MyTelegram.ReadModel.ReadModelLocators;

public class ChatInviteImporterReadModelLocator : IChatInviteImporterReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        switch (aggregateEvent)
        {
            case ChatInviteImportedEvent chatInviteImportedEvent:
                yield return ChatInviteImporterId
                    .Create(chatInviteImportedEvent.ChannelId, chatInviteImportedEvent.RequestInfo.UserId).Value;
                break;
            case ChatInviteCreatedEvent chatInviteCreatedEvent:
                yield return ChatInviteImporterId
                    .Create(chatInviteCreatedEvent.ChannelId, chatInviteCreatedEvent.RequestInfo.UserId).Value;
                break;

            case ChatJoinRequestHiddenEvent chatJoinRequestHiddenEvent:
                yield return ChatInviteImporterId
                    .Create(chatJoinRequestHiddenEvent.ChannelId, chatJoinRequestHiddenEvent.UserId).Value;
                break;
        }
    }
}