namespace MyTelegram.Domain.Commands.Dialog;

public class ReadMentionCommand(DialogId aggregateId, long ownerUserId, int messageId, bool readAllMentions = false)
    : Command<DialogAggregate, DialogId, IExecutionResult>(aggregateId)
{
    public long OwnerUserId { get; } = ownerUserId;

    //public long ToPeerId { get; }
    public int MessageId { get; } = messageId;

    public bool ReadAllMentions { get; } = readAllMentions;

    /*long toPeerId,*/
    //ToPeerId = toPeerId;
}