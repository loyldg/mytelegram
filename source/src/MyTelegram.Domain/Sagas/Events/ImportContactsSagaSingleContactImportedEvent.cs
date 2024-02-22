namespace MyTelegram.Domain.Sagas.Events;

public class ImportContactsSagaSingleContactImportedEvent : AggregateEvent<ImportContactsSaga, ImportContactsSagaId>
{
    public ImportContactsSagaSingleContactImportedEvent(PhoneContact phoneContact)
    {
        PhoneContact = phoneContact;
    }

    public PhoneContact PhoneContact { get; }
}
