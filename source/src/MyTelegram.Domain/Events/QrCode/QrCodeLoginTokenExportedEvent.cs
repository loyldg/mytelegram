namespace MyTelegram.Domain.Events.QrCode;

public class QrCodeLoginTokenExportedEvent : RequestAggregateEvent<QrCodeAggregate, QrCodeId>
{
    public QrCodeLoginTokenExportedEvent(long reqMsgId,
        long tempAuthKeyId,
        long permAuthKeyId,
        byte[] token,
        int expireDate,
        List<long> exceptUidList
    ) : base(reqMsgId)
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
