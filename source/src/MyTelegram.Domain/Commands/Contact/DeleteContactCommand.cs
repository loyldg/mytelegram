namespace MyTelegram.Domain.Commands.Contact;

public class DeleteContactCommand : RequestCommand2<ContactAggregate, ContactId, IExecutionResult>
{
    public DeleteContactCommand(ContactId aggregateId,
        RequestInfo requestInfo,
        long targetUserId) : base(aggregateId, requestInfo)
    {
        TargetUserId = targetUserId;
    }

    public long TargetUserId { get; }
}
