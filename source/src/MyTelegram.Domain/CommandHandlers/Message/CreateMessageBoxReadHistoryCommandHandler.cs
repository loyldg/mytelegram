//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class CreateMessageBoxReadHistoryCommandHandler : CommandHandler<MessageBoxReadHistoryAggregate,
//    MessageBoxReadHistoryId, CreateMessageBoxReadHistoryCommand>
//{
//    public override Task ExecuteAsync(MessageBoxReadHistoryAggregate aggregate,
//        CreateMessageBoxReadHistoryCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.Create(command.UserId, command.PeerId, command.MsgId);
//        return Task.CompletedTask;
//    }
//}


