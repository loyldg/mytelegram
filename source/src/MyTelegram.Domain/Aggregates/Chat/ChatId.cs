namespace MyTelegram.Domain.Aggregates.Chat;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ChatId>))]
public class ChatId : MyIdentity<ChatId>
{
    public ChatId(string value) : base(value)
    {
    }

    public static ChatId Create(long chatId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"chat_{chatId}");
    }
}
