namespace MyTelegram.Domain.Aggregates.Channel;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<ChannelId>))]
public class ChannelId : MyIdentity<ChannelId>
{
    public ChannelId(string value) : base(value)
    {
    }

    public static ChannelId Create(long channelId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"channel_{channelId}");
    }
}
