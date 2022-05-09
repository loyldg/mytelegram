namespace MyTelegram.Domain.Aggregates.UserName;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<UserNameId>))]
public class UserNameId : MyIdentity<UserNameId>
{
    public UserNameId(string value) : base(value)
    {
    }

    public static UserNameId Create( /*PeerType peerType, long peerId, */ string userName)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"username_{userName}");
    }
}
