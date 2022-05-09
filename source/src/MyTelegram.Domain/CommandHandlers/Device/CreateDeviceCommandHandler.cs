namespace MyTelegram.Domain.CommandHandlers.Device;

public class CreateDeviceCommandHandler : CommandHandler<DeviceAggregate, DeviceId, CreateDeviceCommand>
{
    public override Task ExecuteAsync(DeviceAggregate aggregate,
        CreateDeviceCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.Create(command.PermAuthKeyId,
            command.TempAuthKeyId,
            command.UserId,
            command.AppId,
            command.AppName,
            command.AppVersion,
            command.Hash,
            command.OfficialApp,
            command.PasswordPending,
            command.DeviceModel,
            command.Platform,
            command.SystemVersion,
            command.SystemLangCode,
            command.LangPack,
            command.LangCode,
            command.Ip,
            command.Layer);
        return Task.CompletedTask;
    }
}
