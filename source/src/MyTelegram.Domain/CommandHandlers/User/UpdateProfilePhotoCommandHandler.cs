namespace MyTelegram.Domain.CommandHandlers.User;

public class UpdateProfilePhotoCommandHandler : CommandHandler<UserAggregate, UserId, UpdateProfilePhotoCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        UpdateProfilePhotoCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateProfilePhoto(command.RequestInfo,
            command.PhotoId,
            command.Fallback//,
            //command.Photo,
            //command.VideoEmojiMarkup
        );
        return Task.CompletedTask;
    }
}