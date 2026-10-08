namespace MyTelegram.Domain.Aggregates.UserName;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<UserNameId>))]
public class UserNameId(string value) : Identity<UserNameId>(value)
{
    public static UserNameId Create( /*PeerType peerType, long peerId, */ string userName)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"username_{userName.ToLower()}");
    }
}
