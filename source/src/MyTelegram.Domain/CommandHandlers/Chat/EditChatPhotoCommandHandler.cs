namespace MyTelegram.Domain.CommandHandlers.Chat;

public class EditChatPhotoCommandHandler : CommandHandler<ChatAggregate, ChatId, EditChatPhotoCommand>
{
    public override Task ExecuteAsync(ChatAggregate aggregate,
        EditChatPhotoCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditPhoto(command.RequestInfo,
            //command.FileId,
            //command.Photo,
            command.PhotoId,
            command.MessageActionData,
            command.RandomId);
        return Task.CompletedTask;
    }
}