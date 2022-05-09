namespace MyTelegram.Domain.Events.User;

public class UserProfilePhotoChangedEvent : RequestAggregateEvent<UserAggregate, UserId>
{
    //public bool HasVideo { get; }
    //public double VideoStartTs { get; }

    public UserProfilePhotoChangedEvent(long reqMsgId,
        long fileId,
        UserItem userItem /*, bool hasVideo, double videoStartTs*/) : base(reqMsgId)
    {
        FileId = fileId;
        UserItem = userItem;
        //HasVideo = hasVideo;
        //VideoStartTs = videoStartTs;
    }

    public long FileId { get; }
    public UserItem UserItem { get; }
}

//[EventVersion("UserCreatedEvent", 1)]
//[EventVersion("UpdateProfile", 1)]

//[EventVersion("UpdateStatus", 1)]
