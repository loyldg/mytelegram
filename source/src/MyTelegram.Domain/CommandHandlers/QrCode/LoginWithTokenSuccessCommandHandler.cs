namespace MyTelegram.Domain.CommandHandlers.QrCode;

public class
    LoginWithTokenSuccessCommandHandler : CommandHandler<QrCodeAggregate, QrCodeId, LoginWithTokenSuccessCommand>
{
    public override Task ExecuteAsync(QrCodeAggregate aggregate,
        LoginWithTokenSuccessCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.LoginWithTokenSuccess(command.RequestInfo);
        return Task.CompletedTask;
    }
}