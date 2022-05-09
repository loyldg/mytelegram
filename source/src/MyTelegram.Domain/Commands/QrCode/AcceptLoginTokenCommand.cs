namespace MyTelegram.Domain.Commands.QrCode;

public class AcceptLoginTokenCommand : RequestCommand<QrCodeAggregate, QrCodeId, IExecutionResult>
{
    public AcceptLoginTokenCommand(QrCodeId aggregateId,
        long reqMsgId,
        long userId,
        byte[] token) : base(aggregateId, reqMsgId)
    {
        UserId = userId;
        Token = token;
    }

    public byte[] Token { get; }
    public long UserId { get; }
}
