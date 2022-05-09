namespace MyTelegram.ReadModel.ReadModelLocators;

public class ChannelFullReadModelLocator : IChannelFullReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        if (domainEvent.AggregateType == typeof(ChannelAggregate))
        {
            yield return domainEvent.GetIdentity().Value;
        } else
        {
            switch (aggregateEvent)
            {
                case ChannelMemberJoinedEvent channelMemberJoinedEvent:
                    yield return ChannelId.Create(channelMemberJoinedEvent.ChannelId).Value;
                    break;
                case ChannelMemberLeftEvent channelMemberLeftEvent:
                    yield return ChannelId.Create(channelMemberLeftEvent.ChannelId).Value;
                    break;
                case ChannelMemberBannedRightsChangedEvent channelMemberBannedRightsChangedEvent:
                    yield return ChannelId.Create(channelMemberBannedRightsChangedEvent.ChannelId).Value;
                    break;
            }
        }
    }
}