namespace MyTelegram.Domain.Events.User;

public class UserColorUpdatedEvent : RequestAggregateEvent2<UserAggregate, UserId>
{
    public long UserId { get; }
    public PeerColor? Color { get; }
    public bool ForProfile { get; }

    public UserColorUpdatedEvent(RequestInfo requestInfo,long userId,PeerColor? color,bool forProfile /*int color, long? backgroundEmojiId*/) : base(requestInfo)
    {
        UserId = userId;
        Color = color;
        ForProfile = forProfile;
    }
}