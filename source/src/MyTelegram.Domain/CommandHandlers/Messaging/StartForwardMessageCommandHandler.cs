namespace MyTelegram.Domain.CommandHandlers.Messaging;

public class
    StartForwardMessageCommandHandler : CommandHandler<MessageAggregate, MessageId, StartForwardMessageCommand>
{
    public override Task ExecuteAsync(MessageAggregate aggregate,
        StartForwardMessageCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.StartForwardMessage(command.RequestInfo,
            command.FromPeer,
            command.ToPeer,
            command.IdList,
            command.RandomIdList,
            command.ForwardFromLinkedChannel);
        return Task.CompletedTask;
    }
}
