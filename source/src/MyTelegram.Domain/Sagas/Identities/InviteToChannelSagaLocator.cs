namespace MyTelegram.Domain.Sagas.Identities;

public class InviteToChannelSagaLocator : DefaultSagaLocator<InviteToChannelSaga, InviteToChannelSagaId>
{
    protected override InviteToChannelSagaId CreateSagaId(string requestId)
    {
        return new InviteToChannelSagaId(requestId);
    }
}