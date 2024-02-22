namespace MyTelegram.Domain.Commands.Contact;

public class UpdateContactProfilePhotoCommand : RequestCommand2<ContactAggregate, ContactId, IExecutionResult>//, IHasCorrelationId
{
    public long SelfUserId { get; }
    public long TargetUserId { get; }
    public long PhotoId { get; }
    public bool Suggest { get; }
    public string? MessageActionData { get; }

    public UpdateContactProfilePhotoCommand(ContactId aggregateId, RequestInfo requestInfo, long selfUserId, long targetUserId, long photoId, bool suggest, string? messageActionData) : base(aggregateId, requestInfo)
    {
        SelfUserId = selfUserId;
        TargetUserId = targetUserId;
        PhotoId = photoId;
        Suggest = suggest;
        MessageActionData = messageActionData;
    }
}