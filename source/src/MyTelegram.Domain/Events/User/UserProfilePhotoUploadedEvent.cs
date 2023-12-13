namespace MyTelegram.Domain.Events.User;

public class UserProfilePhotoUploadedEvent : RequestAggregateEvent2<UserAggregate, UserId>
{
    //public bool HasVideo { get; }
    //public double VideoStartTs { get; }

    public UserProfilePhotoUploadedEvent(RequestInfo requestInfo,
        long photoId,
        bool fallback,
        //long userId,
        //UserItem userItem,
        VideoSizeEmojiMarkup? videoEmojiMarkup
        /*, bool hasVideo, double videoStartTs*/) : base(requestInfo)
    {
        PhotoId = photoId;
        Fallback = fallback;
        //UserId = userId;
        //UserItem = userItem;
        VideoEmojiMarkup = videoEmojiMarkup;
        //HasVideo = hasVideo;
        //VideoStartTs = videoStartTs;
    }

    public long PhotoId { get; }
    public bool Fallback { get; }
    //public long UserId { get; }
    //public UserItem UserItem { get; }
    public VideoSizeEmojiMarkup? VideoEmojiMarkup { get; }
}