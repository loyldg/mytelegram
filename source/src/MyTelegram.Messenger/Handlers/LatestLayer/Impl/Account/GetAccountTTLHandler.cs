// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get days to live of account
/// See <a href="https://corefork.telegram.org/method/account.getAccountTTL" />
///</summary>
internal sealed class GetAccountTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetAccountTTL, MyTelegram.Schema.IAccountDaysTTL>,
    Account.IGetAccountTTLHandler
{
    private readonly IQueryProcessor _queryProcessor;

    public GetAccountTTLHandler(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    protected override async Task<MyTelegram.Schema.IAccountDaysTTL> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetAccountTTL obj)
    {
        var user = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(input.UserId), default);

        return new TAccountDaysTTL { Days = user!.AccountTtl };
    }
}
