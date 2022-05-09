namespace MyTelegram.Domain.Commands.QrCode;

public class ExportLoginTokenCommand : RequestCommand<QrCodeAggregate, QrCodeId, IExecutionResult>
{
    public ExportLoginTokenCommand(QrCodeId aggregateId,
        long reqMsgId,
        long tempAuthKeyId,
        long permAuthKeyId,
        byte[] token,
        int expireDate,
        List<long> exceptUidList) : base(aggregateId, reqMsgId)
    {
        TempAuthKeyId = tempAuthKeyId;
        PermAuthKeyId = permAuthKeyId;
        Token = token;
        ExpireDate = expireDate;
        ExceptUidList = exceptUidList;
    }

    public List<long> ExceptUidList { get; }
    public int ExpireDate { get; }
    public long PermAuthKeyId { get; }
    public long TempAuthKeyId { get; }
    public byte[] Token { get; }
}
