namespace MyTelegram.Domain.Sagas.Identities;

public class VoteSagaLocator : DefaultSagaLocator<VoteSaga, VoteSagaId>
{
    protected override VoteSagaId CreateSagaId(string requestId)
    {
        return new VoteSagaId(requestId);
    }
}