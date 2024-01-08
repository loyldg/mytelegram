using MyTelegram.Domain.Events.Updates;

namespace MyTelegram.Domain.Aggregates.Updates;

public class UpdatesState : AggregateState<UpdatesAggregate, UpdatesId, UpdatesState>,
    IApply<UpdatesCreatedEvent>
{
    //public long OwnerPeerId { get; private set; }
    //public long? ExcludeAuthKeyId { get; private set; }
    //public long? ExcludeUserId { get; private set; }
    //public long? OnlySendToThisAuthKeyId { get; private set; }
    //public PtsType PtsType { get; private set; }
    //public int Pts { get; private set; }
    //public int? MessageId { get; private set; }
    //public int Date { get; private set; }
    //public long SeqNo { get; private set; }
    //public byte[] Updates { get; private set; }
    //public List<long>? Users { get; private set; }
    //public List<long>? Chats { get; private set; }
    public void Apply(UpdatesCreatedEvent aggregateEvent)
    {

    }
}