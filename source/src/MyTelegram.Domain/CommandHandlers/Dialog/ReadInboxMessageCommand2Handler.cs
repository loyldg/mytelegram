namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class ReadInboxMessageCommand2Handler : CommandHandler<DialogAggregate, DialogId, ReadInboxMessageCommand2>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        ReadInboxMessageCommand2 command,
        CancellationToken cancellationToken)
    {
        aggregate.ReadInboxMessage2(command.RequestInfo,
            command.ReaderUid,
            command.OwnerPeerId,
            command.MaxMessageId,
            command.UnreadCount,
            command.ToPeer,
            command.Date
            );
        return Task.CompletedTask;
    }
}