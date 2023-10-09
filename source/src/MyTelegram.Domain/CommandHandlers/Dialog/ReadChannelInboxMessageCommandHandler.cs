namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class
    ReadChannelInboxMessageCommandHandler : CommandHandler<DialogAggregate, DialogId,
        ReadChannelInboxMessageCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate,
        ReadChannelInboxMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ReadChannelInboxMessage(command.RequestInfo,
            command.ReaderUserId,
            command.ChannelId,
            command.MaxId,
            command.SenderUserId,
            command.TopMsgId);
        return Task.CompletedTask;
    }
}