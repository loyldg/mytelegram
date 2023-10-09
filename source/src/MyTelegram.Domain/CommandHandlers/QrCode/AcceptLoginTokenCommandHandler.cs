namespace MyTelegram.Domain.CommandHandlers.QrCode;

public class AcceptLoginTokenCommandHandler : CommandHandler<QrCodeAggregate, QrCodeId, AcceptLoginTokenCommand>
{
    public override Task ExecuteAsync(QrCodeAggregate aggregate,
        AcceptLoginTokenCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.AcceptLoginToken(command.RequestInfo, command.UserId, command.Token);
        return Task.CompletedTask;
    }
}
