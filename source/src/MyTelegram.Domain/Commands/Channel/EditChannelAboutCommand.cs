namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelAboutCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public EditChannelAboutCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        long selfUserId,
        string? about
    ) : base(aggregateId, requestInfo)
    {
        SelfUserId = selfUserId;
        About = about;
    }

    public string? About { get; }
    public long SelfUserId { get; }
}