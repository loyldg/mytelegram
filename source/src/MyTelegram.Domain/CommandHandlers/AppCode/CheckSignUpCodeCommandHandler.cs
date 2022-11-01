namespace MyTelegram.Domain.CommandHandlers.AppCode;

public class CheckSignUpCodeCommandHandler : CommandHandler<AppCodeAggregate, AppCodeId, CheckSignUpCodeCommand>
{
    public override Task ExecuteAsync(AppCodeAggregate aggregate,
        CheckSignUpCodeCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CheckSignUpCode(command.RequestInfo,
            command.UserId,
            command.PhoneCodeHash,
            command.AccessHash,
            command.PhoneNumber,
            command.FirstName,
            command.LastName,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
