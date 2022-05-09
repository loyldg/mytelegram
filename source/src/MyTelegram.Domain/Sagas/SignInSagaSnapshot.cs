namespace MyTelegram.Domain.Sagas;

public class SignInSagaSnapshot : ISnapshot
{
    public SignInSagaSnapshot(long reqMsgId,
        long tempAuthKeyId,
        long permAuthKeyId)
    {
        ReqMsgId = reqMsgId;
        TempAuthKeyId = tempAuthKeyId;
        PermAuthKeyId = permAuthKeyId;
    }

    public long PermAuthKeyId { get; }

    public long ReqMsgId { get; }
    public long TempAuthKeyId { get; }
}
