namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditExportedChatInviteSagaId>))]

public class EditExportedChatInviteSagaId : SingleValueObject<string>, ISagaId
{
    public EditExportedChatInviteSagaId(string value) : base(value)
    {
    }
}