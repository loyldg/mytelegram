namespace MyTelegram.ReadModel.ReadModelLocators;

public class ChannelReadModelLocator : IChannelReadModelLocator, ITransientDependency
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        if (domainEvent.AggregateType == typeof(ChannelAggregate))
        {
            yield return domainEvent.GetIdentity().Value;
        }
        else
        {
            switch (aggregateEvent)
            {
                case DeleteChannelMessagesCompletedSagaEvent deleteChannelMessagesCompletedEvent:
                    yield return ChannelId.Create(deleteChannelMessagesCompletedEvent.ChannelId).Value;
                    break;
                case DeleteChannelHistoryCompletedSagaEvent deleteChannelHistoryCompletedEvent:
                    yield return ChannelId.Create(deleteChannelHistoryCompletedEvent.ChannelId).Value;
                    break;
                case DeleteReplyMessagesCompletedSagaEvent deleteReplyMessagesCompletedEvent:
                    yield return ChannelId.Create(deleteReplyMessagesCompletedEvent.ChannelId).Value;
                    break;
                //case ChannelMemberJoinedEvent channelMemberJoinedEvent:
                //    yield return ChannelId.Create(channelMemberJoinedEvent.ChannelId).Value;
                //    break;
                case ChannelMemberLeftEvent channelMemberLeftEvent:
                    yield return ChannelId.Create(channelMemberLeftEvent.ChannelId).Value;
                    break;
                case ChannelMemberBannedRightsChangedEvent channelMemberBannedRightsChangedEvent:
                    yield return ChannelId.Create(channelMemberBannedRightsChangedEvent.ChannelId).Value;
                    break;
                case ChannelMemberCreatedEvent channelMemberCreatedEvent:
                    yield return ChannelId.Create(channelMemberCreatedEvent.ChannelId).Value;
                    break;
            }
        }
    }
}