namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class
    ReadChannelInboxMessageCommandHandler : CommandHandler<DialogAggregate, DialogId,
        ReadChannelInboxMessageCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        ReadChannelInboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ReadChannelInboxMessage(command.ReqMsgId,
            command.ReaderUid,
            command.ChannelId,
            command.MaxId,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
