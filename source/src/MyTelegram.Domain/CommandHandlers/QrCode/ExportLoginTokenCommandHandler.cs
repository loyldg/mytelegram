namespace MyTelegram.Domain.CommandHandlers.QrCode;

public class ExportLoginTokenCommandHandler : CommandHandler<QrCodeAggregate, QrCodeId, ExportLoginTokenCommand>
{
    public override Task ExecuteAsync(QrCodeAggregate aggregate,
        ExportLoginTokenCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.ExportLoginToken(command.RequestInfo,
            command.TempAuthKeyId,
            command.PermAuthKeyId,
            command.Token,
            command.ExpireDate,
            command.ExceptUidList);
        return Task.CompletedTask;
    }
}