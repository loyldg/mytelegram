namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelPhotoCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>//, IHasCorrelationId
{
    public EditChannelPhotoCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        long? fileId,
        //byte[] photo,
        string messageActionData,
        long randomId) : base(aggregateId, requestInfo)
    {
        FileId = fileId;
        //Photo = photo;
        MessageActionData = messageActionData;
        RandomId = randomId;
    }

    public long? FileId { get; }
    public string MessageActionData { get; }
    //public byte[] Photo { get; }
    public long RandomId { get; }
}