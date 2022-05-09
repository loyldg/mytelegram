namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditChannelPhotoSagaId>))]
public class EditChannelPhotoSagaId : Identity<EditChannelPhotoSagaId>, ISagaId
{
    public EditChannelPhotoSagaId(string value) : base(value)
    {
    }
}
