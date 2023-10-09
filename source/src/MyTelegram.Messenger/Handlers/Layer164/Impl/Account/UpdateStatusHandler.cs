// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Updates online user status.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// See <a href="https://corefork.telegram.org/method/account.updateStatus" />
///</summary>
internal sealed class UpdateStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateStatus, IBool>,
    Account.IUpdateStatusHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateStatus obj)
    {
        throw new NotImplementedException();
    }
}
