namespace MyTelegram.Domain.Sagas;

public class CreateChannelSagaState : AggregateState<CreateChannelSaga, CreateChannelSagaId, CreateChannelSagaState>,
    IApply<CreateChannelSagaStartedEvent>
{
    public RequestInfo RequestInfo { get; private set; }
    public string MessageActionData { get; private set; }
    public long RandomId { get; private set; }
    public bool MigratedFromChat { get; private set; }
    public void Apply(CreateChannelSagaStartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        MessageActionData = aggregateEvent.MessageActionData;
        RandomId = aggregateEvent.RandomId;
        MigratedFromChat = aggregateEvent.MigratedFromChat;
    }
}