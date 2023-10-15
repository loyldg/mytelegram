namespace MyTelegram.Domain.CommandHandlers.User;

public class UploadProfilePhotoCommandHandler : CommandHandler<UserAggregate, UserId, UploadProfilePhotoCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        UploadProfilePhotoCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UploadProfilePhoto(command.RequestInfo,
            command.PhotoId,
            command.Fallback,
            //command.Photo,
            command.VideoEmojiMarkup
        );
        return Task.CompletedTask;
    }
}