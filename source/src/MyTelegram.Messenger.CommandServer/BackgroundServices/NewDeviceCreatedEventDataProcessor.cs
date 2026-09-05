using EventFlow.Aggregates.ExecutionResults;
using MyTelegram.Domain.Aggregates.Device;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public class NewDeviceCreatedEventDataProcessor(
    IQueuedCommandExecutor<DeviceAggregate, DeviceId, IExecutionResult> queuedCommandExecutor)
    : IDataProcessor<NewDeviceCreatedEvent>, ITransientDependency
{
    public Task ProcessAsync(NewDeviceCreatedEvent eventData, CancellationToken cancellationToken = default)
    {
        var createDeviceCommand = new CreateDeviceCommand(DeviceId.Create(eventData.PermAuthKeyId),
            //eventData.RequestInfo,
            eventData.PermAuthKeyId,
            eventData.TempAuthKeyId,
            eventData.UserId,
            eventData.ApiId,
            eventData.AppVersion,
            eventData.AppVersion,
            //eventData.Hash,
            eventData.OfficialApp,
            eventData.PasswordPending,
            eventData.DeviceModel,
            eventData.Platform,
            eventData.SystemVersion,
            eventData.SystemLangCode,
            eventData.LangPack,
            eventData.LangCode,
            eventData.Ip,
            eventData.Layer,
            eventData.Parameters
        );
        //await commandBus.PublishAsync(createDeviceCommand, default);
        queuedCommandExecutor.Enqueue(createDeviceCommand);

        return Task.CompletedTask;
    }
}