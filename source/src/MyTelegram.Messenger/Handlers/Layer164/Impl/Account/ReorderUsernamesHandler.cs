// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Reorder usernames associated with the currently logged-in user.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ORDER_INVALID The specified username order is invalid.
/// See <a href="https://corefork.telegram.org/method/account.reorderUsernames" />
///</summary>
internal sealed class ReorderUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestReorderUsernames, IBool>,
    Account.IReorderUsernamesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestReorderUsernames obj)
    {
        throw new NotImplementedException();
    }
}
