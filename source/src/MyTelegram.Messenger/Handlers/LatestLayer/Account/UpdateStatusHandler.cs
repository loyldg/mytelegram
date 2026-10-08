namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Updates online user status.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateStatus"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateStatusHandler(IUserStatusCacheAppService userStatusAppService) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateStatus, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, RequestUpdateStatus obj)
    {
        await userStatusAppService.UpdateStatusAsync(input.UserId, !obj.Offline);

        return new TBoolTrue();
    }
}