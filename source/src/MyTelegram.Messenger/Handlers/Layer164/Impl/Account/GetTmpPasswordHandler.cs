// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get temporary payment password
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// 400 TMP_PASSWORD_DISABLED The temporary password is disabled.
/// See <a href="https://corefork.telegram.org/method/account.getTmpPassword" />
///</summary>
internal sealed class GetTmpPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetTmpPassword, MyTelegram.Schema.Account.ITmpPassword>,
    Account.IGetTmpPasswordHandler
{
    protected override Task<MyTelegram.Schema.Account.ITmpPassword> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetTmpPassword obj)
    {
        throw new NotImplementedException();
    }
}
