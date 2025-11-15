namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Pause or unpause a specific chat, temporarily disconnecting it from all <a href="https://corefork.telegram.org/api/bots/connected-business-bots">business bots »</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.toggleConnectedBotPaused"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleConnectedBotPausedHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestToggleConnectedBotPaused, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestToggleConnectedBotPaused obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}