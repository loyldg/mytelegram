namespace MyTelegram.Domain.Events.User;

public class UserProfilePhotoChangedEvent : RequestAggregateEvent2<UserAggregate, UserId>
{
    //public bool HasVideo { get; }
    //public double VideoStartTs { get; }

    public UserProfilePhotoChangedEvent(RequestInfo requestInfo,
        long userId,
        long photoId,
        bool fallback//,
        //UserItem userItem//,
        //VideoSizeEmojiMarkup? videoEmojiMarkup
        /*, bool hasVideo, double videoStartTs*/) : base(requestInfo)
    {
        UserId = userId;
        PhotoId = photoId;
        Fallback = fallback;
        //UserItem = userItem;
        //VideoEmojiMarkup = videoEmojiMarkup;
        //HasVideo = hasVideo;
        //VideoStartTs = videoStartTs;
    }

    public long UserId { get; }
    public long PhotoId { get; }
    public bool Fallback { get; }
    //public UserItem UserItem { get; }
    //public VideoSizeEmojiMarkup? VideoEmojiMarkup { get; }
}

//[EventVersion("UserCreatedEvent", 1)]
//[EventVersion("UpdateProfile", 1)]

//[EventVersion("UpdateStatus", 1)]