//namespace MyTelegram.Domain.CommandHandlers.Message;

//public class IncrementViewsCommandHandler : CommandHandler<MessageBoxAggregate, MessageBoxId, IncrementViewsCommand>
//{
//    public override Task ExecuteAsync(MessageBoxAggregate aggregate,
//        IncrementViewsCommand command,
//        CancellationToken cancellationToken)
//    {
//        aggregate.IncrementViews(command.MessageId);
//        //aggregate.IncrementViews(command.ReqMsgId, command.MessageId, command.AlreadyIncremented, command.CorrelationId);
//        return Task.CompletedTask;
//    }
//}


