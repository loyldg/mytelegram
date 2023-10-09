namespace MyTelegram.Domain.Sagas;

public class CreateChannelSagaStartedEvent : AggregateEvent<CreateChannelSaga, CreateChannelSagaId>
{
    public RequestInfo RequestInfo { get; }
    public string MessageActionData { get; }
    public long RandomId { get; }
    public bool MigratedFromChat { get; }

    public CreateChannelSagaStartedEvent(RequestInfo requestInfo, string messageActionData, long randomId, bool migratedFromChat)
    {
        RequestInfo = requestInfo;
        MessageActionData = messageActionData;
        RandomId = randomId;
        MigratedFromChat = migratedFromChat;
    }
}