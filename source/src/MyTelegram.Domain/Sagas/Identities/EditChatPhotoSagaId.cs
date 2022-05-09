namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditChatPhotoSagaId>))]
public class EditChatPhotoSagaId : Identity<EditChatPhotoSagaId>, ISagaId
{
    public EditChatPhotoSagaId(string value) : base(value)
    {
    }
}
