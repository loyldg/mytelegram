namespace MyTelegram.Domain.Sagas.Identities;

public class ImportChatInviteSagaLocator : DefaultSagaLocator<ImportChatInviteSaga, ImportChatInviteSagaId>
{
    protected override ImportChatInviteSagaId CreateSagaId(string sagaId)
    {
        return new ImportChatInviteSagaId(sagaId);
    }
}