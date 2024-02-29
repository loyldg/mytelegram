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
            command.ApiId,
            command.AppName,
            command.AppVersion,
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
