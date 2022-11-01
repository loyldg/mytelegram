namespace MyTelegram.Domain.CommandHandlers.Channel;

public class EditChannelPhotoCommandHandler : CommandHandler<ChannelAggregate, ChannelId, EditChannelPhotoCommand>
{
    public override Task ExecuteAsync(ChannelAggregate aggregate,
        EditChannelPhotoCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.EditPhoto(command.RequestInfo,
            //command.FileId,
            command.Photo,
            command.MessageActionData,
            command.RandomId,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
