namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Get temporary payment password
/// Possible errors
/// Code Type Description
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// 400 SRP_A_INVALID The specified inputCheckPasswordSRP.A value is invalid.
/// 400 TMP_PASSWORD_DISABLED The temporary password is disabled.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getTmpPassword"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetTmpPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetTmpPassword, MyTelegram.Schema.Account.ITmpPassword>
{
    protected override Task<MyTelegram.Schema.Account.ITmpPassword> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetTmpPassword obj)
    {
        throw new NotImplementedException();
    }
}