namespace MyTelegram.Domain.Commands.User;

public class UploadProfilePhotoCommand : RequestCommand2<UserAggregate, UserId, IExecutionResult>
{
    //public byte[]? Photo { get; }
    //public VideoSizeEmojiMarkup? VideoEmojiMarkup { get; }

    public UploadProfilePhotoCommand(UserId aggregateId,
        RequestInfo requestInfo,
        //long fileId,
        long photoId,
        bool fallback,
        byte[]? photo,
        VideoSizeEmojiMarkup? videoEmojiMarkup = null
    ) : base(aggregateId, requestInfo)
    {
        PhotoId = photoId;
        Fallback = fallback;
        Photo = photo;
        VideoEmojiMarkup = videoEmojiMarkup;
    }

    public long PhotoId { get; }
    public bool Fallback { get; }
    public byte[]? Photo { get; }
    public VideoSizeEmojiMarkup? VideoEmojiMarkup { get; }
}