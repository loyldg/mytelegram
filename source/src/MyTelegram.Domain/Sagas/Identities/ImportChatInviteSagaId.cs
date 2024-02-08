namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ImportChatInviteSagaId>))]
public class ImportChatInviteSagaId : SingleValueObject<string>, ISagaId
{
    public ImportChatInviteSagaId(string value) : base(value)
    {
    }
}