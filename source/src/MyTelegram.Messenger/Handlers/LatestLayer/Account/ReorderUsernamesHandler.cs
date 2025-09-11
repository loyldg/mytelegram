namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Reorder usernames associated with the currently logged-in user.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ORDER_INVALID The specified username order is invalid.
/// 400 USERNAME_NOT_MODIFIED The username was not modified.
/// See <a href="https://corefork.telegram.org/method/account.reorderUsernames" />
///</summary>
internal sealed class ReorderUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestReorderUsernames, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestReorderUsernames obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
