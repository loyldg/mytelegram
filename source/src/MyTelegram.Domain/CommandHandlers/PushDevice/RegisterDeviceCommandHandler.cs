namespace MyTelegram.Domain.CommandHandlers.PushDevice;

public class RegisterDeviceCommandHandler : CommandHandler<PushDeviceAggregate, PushDeviceId, RegisterDeviceCommand>
{
    public override Task ExecuteAsync(PushDeviceAggregate aggregate,
        RegisterDeviceCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.RegisterDevice(command.RequestInfo,
            command.UserId,
            command.AuthKeyId,
            command.TokenType,
            command.Token,
            command.NoMuted,
            command.AppSandbox,
            command.Secret,
            command.OtherUids);
        return Task.CompletedTask;
    }
}
