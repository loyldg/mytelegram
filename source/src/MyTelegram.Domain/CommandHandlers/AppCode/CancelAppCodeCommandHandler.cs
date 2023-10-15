namespace MyTelegram.Domain.CommandHandlers.AppCode;

public class CancelAppCodeCommandHandler : CommandHandler<AppCodeAggregate, AppCodeId, CancelAppCodeCommand>
{
    public override Task ExecuteAsync(AppCodeAggregate aggregate,
        CancelAppCodeCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CancelCode(command.RequestInfo, command.PhoneNumber, command.PhoneCodeHash);

        return Task.CompletedTask;
    }
}
