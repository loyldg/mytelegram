namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Connect a <a href="https://corefork.telegram.org/api/business#connected-bots">business bot »</a> to the current account, or to change the current connection settings.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_BUSINESS_MISSING The specified bot is not a business bot (the <a href="https://corefork.telegram.org/constructor/user">user</a>.<code>bot_business</code> flag is not set).
/// 400 BUSINESS_RECIPIENTS_EMPTY You didn't set any flag in inputBusinessBotRecipients, thus the bot cannot work with <em>any</em> peer.
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// See <a href="https://corefork.telegram.org/method/account.updateConnectedBot" />
///</summary>
internal sealed class UpdateConnectedBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateConnectedBot, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateConnectedBot obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Chats = [],
            Users = [],
            Updates = [],
            Date = CurrentDate
        });
    }
}
