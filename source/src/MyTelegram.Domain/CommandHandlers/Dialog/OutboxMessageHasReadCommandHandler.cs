namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class
    OutboxMessageHasReadCommandHandler : CommandHandler<DialogAggregate, DialogId, OutboxMessageHasReadCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        OutboxMessageHasReadCommand command,
        CancellationToken cancellationToken)
    {
        //Console.WriteLine($"outbox has read:OwnerPeerId:{command.OwnerPeerId} MaxMessageId:{command.MaxMessageId}");
        aggregate.OutboxMessageHasRead(command.ReqMsgId, command.MaxMessageId, command.OwnerPeerId, command.CorrelationId);
        return Task.CompletedTask;
    }
}
