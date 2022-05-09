namespace MyTelegram.Domain.Aggregates.User;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<UserId>))]
public class UserId : MyIdentity<UserId>
{
    public UserId(string value) : base(value)
    {
    }

    public static UserId Create(long userId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"user_{userId}");
    }
}
