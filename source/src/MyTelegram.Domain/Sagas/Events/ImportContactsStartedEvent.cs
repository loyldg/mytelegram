namespace MyTelegram.Domain.Sagas.Events;

public class ImportContactsStartedEvent : RequestAggregateEvent2<ImportContactsSaga, ImportContactsSagaId>
{
    public ImportContactsStartedEvent(RequestInfo requestInfo,
        int count):base(requestInfo)
    {
        Count = count;
    }

    public int Count { get; }
}
