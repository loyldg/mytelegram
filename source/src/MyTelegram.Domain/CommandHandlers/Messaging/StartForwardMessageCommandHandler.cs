namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class
    StartForwardMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, StartForwardMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        StartForwardMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartForwardMessage(command.Request,
            command.FromPeer,
            command.ToPeer,
            command.IdList,
            command.RandomIdList,
            command.ForwardFromLinkedChannel,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}