namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Set or remove the <a href="https://corefork.telegram.org/api/business#business-introduction">Telegram Business introduction »</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateBusinessIntro"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateBusinessIntroHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessIntro, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUpdateBusinessIntro obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}