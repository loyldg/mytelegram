namespace MyTelegram.Domain.Commands.QrCode;

public class LoginWithTokenSuccessCommand : RequestCommand<QrCodeAggregate, QrCodeId, IExecutionResult>
{
    public LoginWithTokenSuccessCommand(QrCodeId aggregateId,
        long reqMsgId) : base(aggregateId, reqMsgId)
    {
    }
}
