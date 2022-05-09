namespace MyTelegram.Domain.CommandHandlers.User;

public class UpdateProfilePhotoCommandHandler : CommandHandler<UserAggregate, UserId, UpdateProfilePhotoCommand>
{
    public override Task ExecuteAsync(UserAggregate aggregate,
        UpdateProfilePhotoCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdateProfilePhoto(command.ReqMsgId,
            command.FileId,
            command.Photo /*,command.HasVideo,command.VideoStartTs*/);
        return Task.CompletedTask;
    }
}
