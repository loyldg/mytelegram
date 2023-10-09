namespace MyTelegram.Domain.Commands.User;

public class UpdateProfilePhotoCommand : RequestCommand2<UserAggregate, UserId, IExecutionResult>
{
    public UpdateProfilePhotoCommand(UserId aggregateId,
        RequestInfo requestInfo,
        //long fileId,
        long photoId,
        bool fallback//,
        //byte[]? photo,
        //VideoSizeEmojiMarkup? videoEmojiMarkup = null
    ) : base(aggregateId, requestInfo)
    {
        PhotoId = photoId;
        Fallback = fallback;
        //Photo = photo;
        //VideoEmojiMarkup = videoEmojiMarkup;
    }

    public long PhotoId { get; }
    public bool Fallback { get; }
}