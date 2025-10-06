namespace MyTelegram.Domain.CommandHandlers.Dialog;

public class ReadMentionCommandHandler : CommandHandler<DialogAggregate, DialogId, ReadMentionCommand>
{
    public override Task ExecuteAsync(DialogAggregate aggregate, ReadMentionCommand command, CancellationToken cancellationToken)
    {
        aggregate.ReadMention(command.MessageId, command.ReadAllMentions);

        return Task.CompletedTask;
    }
}