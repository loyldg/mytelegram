namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Permanently disconnect a specific chat from all <a href="https://corefork.telegram.org/api/business#connected-bots">business bots »</a> (equivalent to specifying it in <code>recipients.exclude_users</code> during initial configuration with <a href="https://corefork.telegram.org/method/account.updateConnectedBot">account.updateConnectedBot »</a>); to reconnect of a chat disconnected using this method the user must reconnect the entire bot by invoking <a href="https://corefork.telegram.org/method/account.updateConnectedBot">account.updateConnectedBot »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_ALREADY_DISABLED The connected business bot was already disabled for the specified peer.
/// 400 BOT_NOT_CONNECTED_YET No <a href="https://corefork.telegram.org/api/business#connected-bots">business bot</a> is connected to the currently logged in user.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/account.disablePeerConnectedBot" />
///</summary>
internal sealed class DisablePeerConnectedBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDisablePeerConnectedBot, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestDisablePeerConnectedBot obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
