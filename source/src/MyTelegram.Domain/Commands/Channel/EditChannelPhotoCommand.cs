namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelPhotoCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>, IHasCorrelationId
{
    public EditChannelPhotoCommand(ChannelId aggregateId,
        RequestInfo request,
        long fileId,
        byte[] photo,
        string messageActionData,
        long randomId,
        Guid correlationId) : base(aggregateId, request)
    {
        FileId = fileId;
        Photo = photo;
        MessageActionData = messageActionData;
        RandomId = randomId;
        CorrelationId = correlationId;
    }

    public long FileId { get; }
    public string MessageActionData { get; }
    public byte[] Photo { get; }
    public long RandomId { get; }
    public Guid CorrelationId { get; }
}
