namespace MyTelegram.Domain.Commands.Messaging;

public class StartUpdatePinnedMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public StartUpdatePinnedMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date,
        long randomId,
        string messageActionData) : base(aggregateId, requestInfo)
    {
        Pinned = pinned;
        PmOneSide = pmOneSide;
        Silent = silent;
        Date = date;
        RandomId = randomId;
        MessageActionData = messageActionData;
    }

    public bool Pinned { get; }
    public bool PmOneSide { get; }
    public bool Silent { get; }
    public int Date { get; }
    public long RandomId { get; }
    public string MessageActionData { get; }
}
