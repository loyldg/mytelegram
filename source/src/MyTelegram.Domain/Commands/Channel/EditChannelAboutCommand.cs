namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelAboutCommand : RequestCommand<ChannelAggregate, ChannelId, IExecutionResult>
{
    public EditChannelAboutCommand(ChannelId aggregateId,
        long reqMsgId,
        long selfUserId,
        string? about
    ) : base(aggregateId, reqMsgId)
    {
        SelfUserId = selfUserId;
        About = about;
    }

    public string? About { get; }
    public long SelfUserId { get; }
}
