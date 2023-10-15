using EventFlow.Exceptions;
using MyTelegram.Domain.Aggregates.Device;
using MyTelegram.Domain.Commands.Device;
using MyTelegram.Domain.Commands.User;
using MyTelegram.Schema.Extensions;

namespace MyTelegram.Messenger.CommandServer.EventHandlers;

public class MessengerEventHandler :
    IEventHandler<MessengerCommandDataReceivedEvent>,
    IEventHandler<NewDeviceCreatedEvent>,
    IEventHandler<BindUidToAuthKeyIntegrationEvent>,
    IEventHandler<AuthKeyUnRegisteredIntegrationEvent>
{
    private readonly ICommandBus _commandBus;
    private readonly ILogger<MessengerEventHandler> _logger;
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly IMessageQueueProcessor<MessengerCommandDataReceivedEvent> _processor;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRandomHelper _randomHelper;

    private static long _count = 0;

    public MessengerEventHandler(ICommandBus commandBus,
        IMessageQueueProcessor<MessengerCommandDataReceivedEvent> processor,
        IObjectMessageSender objectMessageSender,
        ILogger<MessengerEventHandler> logger,
        IQueryProcessor queryProcessor,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _processor = processor;
        _objectMessageSender = objectMessageSender;
        _logger = logger;
        _queryProcessor = queryProcessor;
        _randomHelper = randomHelper;
    }

    public Task HandleEventAsync(AuthKeyUnRegisteredIntegrationEvent eventData)
    {
        var command = new UnRegisterDeviceForAuthKeyCommand(DeviceId.Create(eventData.PermAuthKeyId),
            eventData.PermAuthKeyId,
            eventData.TempAuthKeyId);
        return _commandBus.PublishAsync(command, default);
    }

    public Task HandleEventAsync(BindUidToAuthKeyIntegrationEvent eventData)
    {
        var command = new BindUidToDeviceCommand(DeviceId.Create(eventData.PermAuthKeyId),
            eventData.UserId,
            eventData.PermAuthKeyId);
        return _commandBus.PublishAsync(command, default);
    }

    public Task HandleEventAsync(MessengerCommandDataReceivedEvent eventData)
    {
        _processor.Enqueue(eventData, eventData.PermAuthKeyId);
        return Task.CompletedTask;
    }

    public async Task HandleEventAsync(NewDeviceCreatedEvent eventData)
    {
        try
        {
            var createDeviceCommand = new CreateDeviceCommand(DeviceId.Create(eventData.PermAuthKeyId),
                eventData.RequestInfo,
                eventData.PermAuthKeyId,
                eventData.TempAuthKeyId,
                eventData.UserId,
                eventData.AppId,
                eventData.AppVersion,
                eventData.AppVersion,
                eventData.Hash,
                eventData.OfficialApp,
                eventData.PasswordPending,
                eventData.DeviceModel,
                eventData.Platform,
                eventData.SystemVersion,
                eventData.SystemLangCode,
                eventData.LangPack,
                eventData.LangCode,
                eventData.Ip,
                eventData.Layer
            );
            await _commandBus.PublishAsync(createDeviceCommand, default);
        }
        catch (DuplicateOperationException)
        {
            // Ignore duplicate exception
        }
    }

    public Task HandleEventAsync(UserIsOnlineEvent eventData)
    {
        _logger.LogDebug("User {UserId} is online,tempAuthKeyId={TempAuthKeyId:x2},permAuthKeyId={PermAuthKeyId:x2}",
            eventData.UserId,
            eventData.TempAuthKeyId,
            eventData.PermAuthKeyId);
        var updatesTooLong = new TUpdatesTooLong();
        return _objectMessageSender.PushSessionMessageToAuthKeyIdAsync(eventData.TempAuthKeyId, updatesTooLong);
    }
}
