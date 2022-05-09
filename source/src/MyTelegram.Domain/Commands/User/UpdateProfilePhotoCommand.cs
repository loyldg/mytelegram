namespace MyTelegram.Domain.Commands.User;

public class UpdateProfilePhotoCommand : RequestCommand<UserAggregate, UserId, IExecutionResult>
{
    public UpdateProfilePhotoCommand(UserId aggregateId,
        long reqMsgId,
        long fileId,
        byte[] photo) : base(aggregateId, reqMsgId)
    {
        FileId = fileId;
        Photo = photo;
    }

    public long FileId { get; }
    public byte[] Photo { get; }
}
