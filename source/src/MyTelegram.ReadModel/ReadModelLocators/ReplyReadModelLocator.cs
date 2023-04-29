namespace MyTelegram.ReadModel.ReadModelLocators;

public class ReplyReadModelLocator : IReplyReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        if (domainEvent.AggregateType == typeof(MessageAggregate))
        {
            yield return domainEvent.GetIdentity().Value;
        }
        else if (domainEvent.AggregateType == typeof(MessageSaga))
        {
            switch (aggregateEvent)
            {
                case ReplyToChannelMessageCompletedEvent replyToChannelMessageCompletedEvent:
                    yield return MessageId.Create(replyToChannelMessageCompletedEvent.ChannelId,
                        replyToChannelMessageCompletedEvent.ReplyToMsgId).Value;
                    break;
            }
        }
    }
}
