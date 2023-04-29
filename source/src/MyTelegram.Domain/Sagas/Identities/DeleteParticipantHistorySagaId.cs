namespace MyTelegram.Domain.Sagas.Identities;

public class DeleteParticipantHistorySagaId : SingleValueObject<string>, ISagaId
{
    public DeleteParticipantHistorySagaId(string value) : base(value)
    {
    }
}
