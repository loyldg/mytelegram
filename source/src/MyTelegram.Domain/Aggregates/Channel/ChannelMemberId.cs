namespace MyTelegram.Domain.Aggregates.Channel;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ChannelMemberId>))]
public class ChannelMemberId : MyIdentity<ChannelMemberId>
{
    public ChannelMemberId(string value) : base(value)
    {
    }

    public static ChannelMemberId Create(long channelId,
        long userId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"channelMember_{channelId}_{userId}");
    }
}
