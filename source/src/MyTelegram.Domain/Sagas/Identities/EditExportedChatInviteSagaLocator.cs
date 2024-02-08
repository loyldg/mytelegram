namespace MyTelegram.Domain.Sagas.Identities;

public class EditExportedChatInviteSagaLocator : DefaultSagaLocator<EditExportedChatInviteSaga, EditExportedChatInviteSagaId>
{
    protected override EditExportedChatInviteSagaId CreateSagaId(string requestId)
    {
        return new EditExportedChatInviteSagaId(requestId);
    }
}