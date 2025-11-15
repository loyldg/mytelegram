namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Set a list of <a href="https://corefork.telegram.org/api/business#away-messages">Telegram Business away messages</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateBusinessAwayMessage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateBusinessAwayMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessAwayMessage, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUpdateBusinessAwayMessage obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}