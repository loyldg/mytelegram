namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class
    ReceiveInboxMessageCommandHandler : CommandHandler<DialogAggregate, DialogId, ReceiveInboxMessageCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        ReceiveInboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ReceiveInboxMessage(command.MessageId,
            command.OwnerPeerId,
            //command.Pts,
            command.ToPeer,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
