namespace MyTelegram.Domain.Commands.Dialog;

public class ReadMentionCommand(
        DialogId aggregateId,
        RequestInfo requestInfo,
        long ownerUserId,
        int messageId,
        bool readAllMentions = false)
    : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>(aggregateId, requestInfo)
{
    public long OwnerUserId { get; } = ownerUserId;

    //public long ToPeerId { get; }
    public int MessageId { get; } = messageId;

    public bool ReadAllMentions { get; } = readAllMentions;

    /*long toPeerId,*/
    //ToPeerId = toPeerId;
}