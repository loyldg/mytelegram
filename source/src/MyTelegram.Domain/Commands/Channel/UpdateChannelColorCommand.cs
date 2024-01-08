namespace MyTelegram.Domain.Commands.Channel;

public class UpdateChannelColorCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public PeerColor Color { get; }
    public long? BackgroundEmojiId { get; }
    public bool ForProfile { get; }

    public UpdateChannelColorCommand(ChannelId aggregateId, RequestInfo requestInfo, PeerColor color, long? backgroundEmojiId, bool forProfile) : base(aggregateId, requestInfo)
    {
        Color = color;
        BackgroundEmojiId = backgroundEmojiId;
        ForProfile = forProfile;
    }
}