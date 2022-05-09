namespace MyTelegram.Domain.CommandHandlers.PushDevice;

public class
    UnRegisterDeviceCommandHandler : CommandHandler<PushDeviceAggregate, PushDeviceId, UnRegisterDeviceCommand>
{
    public override Task ExecuteAsync(PushDeviceAggregate aggregate,
        UnRegisterDeviceCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UnRegisterDevice(command.ReqMsgId, command.TokenType, command.Token, command.OtherUids);
        return Task.CompletedTask;
    }
}
