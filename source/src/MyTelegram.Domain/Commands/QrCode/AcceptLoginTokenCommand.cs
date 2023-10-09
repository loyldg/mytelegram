namespace MyTelegram.Domain.Commands.QrCode;

public class AcceptLoginTokenCommand : RequestCommand2<QrCodeAggregate, QrCodeId, IExecutionResult>
{
    public AcceptLoginTokenCommand(QrCodeId aggregateId,
        RequestInfo requestInfo,
        long userId,
        byte[] token) : base(aggregateId, requestInfo)
    {
        UserId = userId;
        Token = token;
    }

    public byte[] Token { get; }
    public long UserId { get; }
}