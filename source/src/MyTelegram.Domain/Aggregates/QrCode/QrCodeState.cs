namespace MyTelegram.Domain.Aggregates.QrCode;

public class QrCodeState : AggregateState<QrCodeAggregate, QrCodeId, QrCodeState>,
    IApply<QrCodeLoginTokenExportedEvent>,
    IApply<LoginTokenAcceptedEvent>,
    IApply<QrCodeLoginSuccessEvent>
{
    public List<long>? ExceptUidList { get; private set; }
    public int ExpireDate { get; private set; }
    public bool IsAccepted { get; private set; }
    public bool IsLoginTokenUsed { get; private set; }
    public long PermAuthKeyId { get; private set; }
    public long TempAuthKeyId { get; private set; }
    public byte[] Token { get; private set; } = default!;
    public long UserId { get; private set; }

    public void Apply(LoginTokenAcceptedEvent aggregateEvent)
    {
        UserId = aggregateEvent.UserId;
        IsAccepted = true;
    }

    public void Apply(QrCodeLoginSuccessEvent aggregateEvent)
    {
        IsLoginTokenUsed = true;
    }

    public void Apply(QrCodeLoginTokenExportedEvent aggregateEvent)
    {
        TempAuthKeyId = aggregateEvent.TempAuthKeyId;
        PermAuthKeyId = aggregateEvent.PermAuthKeyId;
        ExpireDate = aggregateEvent.ExpireDate;
        Token = aggregateEvent.Token;
        ExceptUidList = aggregateEvent.ExceptUidList;
    }
}
