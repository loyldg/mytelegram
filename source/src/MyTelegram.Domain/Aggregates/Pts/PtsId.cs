using MyTelegram.Domain.Aggregates.User;

namespace MyTelegram.Domain.Aggregates.Pts;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<PtsId>))]
public class PtsId : MyIdentity<PtsId>
{
    public PtsId(string value) : base(value)
    {
    }

    public static PtsId Create(long peerId,
        long permAuthKeyId = 0)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"pts_{peerId}_{permAuthKeyId}");
    }

    public static PtsId CreateChannelPtsId(long userId, long channelId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"pts_{userId}_{channelId}");
    }
}
