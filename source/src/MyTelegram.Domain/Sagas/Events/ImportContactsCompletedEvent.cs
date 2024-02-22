namespace MyTelegram.Domain.Sagas.Events;

public class ImportContactsCompletedEvent : RequestAggregateEvent2<ImportContactsSaga, ImportContactsSagaId>
{
    public ImportContactsCompletedEvent(RequestInfo requestInfo,
        List<PhoneContact> phoneContacts) : base(requestInfo)
    {
        PhoneContacts = phoneContacts;
    }

    public List<PhoneContact> PhoneContacts { get; }
}
