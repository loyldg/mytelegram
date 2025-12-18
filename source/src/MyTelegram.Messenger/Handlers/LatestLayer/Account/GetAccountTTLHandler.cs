namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Get days to live of account
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getAccountTTL"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetAccountTTLHandler(IUserAppService userAppService) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetAccountTTL, MyTelegram.Schema.IAccountDaysTTL>
{
    protected override async Task<MyTelegram.Schema.IAccountDaysTTL> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetAccountTTL obj)
    {
        var user = await userAppService.GetAsync(input.UserId);
        return new TAccountDaysTTL
        {
            Days = user!.AccountTtl
        };
    }
}