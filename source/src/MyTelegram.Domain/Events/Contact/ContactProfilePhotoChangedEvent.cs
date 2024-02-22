namespace MyTelegram.Domain.Events.Contact;

public class ContactProfilePhotoChangedEvent : RequestAggregateEvent2<ContactAggregate, ContactId>
{
    public long SelfUserId { get; }
    public long TargetUserId { get; }
    public long PhotoId { get; }
    public bool Suggest { get; }
    public string? MessageActionData { get; }

    public ContactProfilePhotoChangedEvent(RequestInfo requestInfo,
        long selfUserId,
        long targetUserId,
        long photoId,
        bool suggest,
        string? messageActionData) : base(requestInfo)
    {
        SelfUserId = selfUserId;
        TargetUserId = targetUserId;
        PhotoId = photoId;
        Suggest = suggest;
        MessageActionData = messageActionData;
        
    }

    
}