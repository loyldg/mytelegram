namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ReadMentionsSagaId>))]
public class ReadMentionsSagaId(string value) : SingleValueObject<string>(value), ISagaId;
