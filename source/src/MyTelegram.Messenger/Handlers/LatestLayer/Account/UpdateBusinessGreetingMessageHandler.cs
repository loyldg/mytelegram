namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Set a list of <a href="https://corefork.telegram.org/api/business#greeting-messages">Telegram Business greeting messages</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateBusinessGreetingMessage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateBusinessGreetingMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessGreetingMessage, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUpdateBusinessGreetingMessage obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}