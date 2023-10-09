namespace MyTelegram.Domain.Commands.QrCode;

public class LoginWithTokenSuccessCommand : RequestCommand2<QrCodeAggregate, QrCodeId, IExecutionResult>
{
    public LoginWithTokenSuccessCommand(QrCodeId aggregateId, RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {
    }
}