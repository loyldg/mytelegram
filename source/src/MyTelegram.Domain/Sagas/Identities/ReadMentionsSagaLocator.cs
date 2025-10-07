namespace MyTelegram.Domain.Sagas.Identities;

public class ReadMentionsSagaLocator : DefaultSagaLocator<ReadMentionsSaga, ReadMentionsSagaId>
{
    protected override ReadMentionsSagaId CreateSagaId(string requestId)
    {
        return new ReadMentionsSagaId(requestId);
    }
}
