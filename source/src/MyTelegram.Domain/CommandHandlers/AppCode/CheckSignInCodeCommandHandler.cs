namespace MyTelegram.Domain.CommandHandlers.AppCode;

public class CheckSignInCodeCommandHandler : CommandHandler<AppCodeAggregate, AppCodeId, CheckSignInCodeCommand>
{
    public override Task ExecuteAsync(AppCodeAggregate aggregate,
        CheckSignInCodeCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.CheckSignInCode(command.RequestInfo,
            command.Code,
            command.UserId,
            command.CorrelationId);
        return Task.CompletedTask;
    }
}
