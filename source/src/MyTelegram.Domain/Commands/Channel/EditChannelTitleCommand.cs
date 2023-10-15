namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelTitleCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>//, IHasCorrelationId
{
    public EditChannelTitleCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        string title,
        string messageActionData,
        long randomId) : base(aggregateId, requestInfo)
    {
        Title = title;
        MessageActionData = messageActionData;
        RandomId = randomId;
    }

    public string MessageActionData { get; }
    public long RandomId { get; }
    public string Title { get; }
}