namespace MyTelegram.Domain.CommandHandlers.Chat;

public class EditChatPhotoCommandHandler : CommandHandler<ChatAggregate, ChatId, EditChatPhotoCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        EditChatPhotoCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditPhoto(command.RequestInfo,
            //command.FileId,
            command.Photo,
            command.MessageActionData,
            command.RandomId,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
