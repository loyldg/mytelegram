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
    private readonly IUserStatusCacheAppService _userStatusAppService;

    public UpdateStatusHandler(IUserStatusCacheAppService userStatusAppService)
    {
        _userStatusAppService = userStatusAppService;
    }

    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateStatus obj)
    {
        //var updateStatusCommand = new UpdateStatusCommand(UserId.Create(input.UserId), input.ReqMsgId, obj.Offline);
        //await CommandBus.PublishAsync(updateStatusCommand, CancellationToken.None);

        // 由于用户状态可能会频繁更新,所以将用户状态信息直接保存到内存中
        _userStatusAppService.UpdateStatus(input.UserId, !obj.Offline);

        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
