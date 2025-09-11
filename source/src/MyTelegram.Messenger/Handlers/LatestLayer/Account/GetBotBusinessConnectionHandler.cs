namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Bots may invoke this method to re-fetch the <a href="https://corefork.telegram.org/constructor/updateBotBusinessConnect">updateBotBusinessConnect</a> constructor associated with a specific <a href="https://corefork.telegram.org/api/business#connected-bots">business <code>connection_id</code>, see here »</a> for more info on connected business bots.<br>
/// This is needed for example for freshly logged in bots that are receiving some <a href="https://corefork.telegram.org/constructor/updateBotNewBusinessMessage">updateBotNewBusinessMessage</a>, etc. updates because some users have already connected to the bot before it could login.<br>
/// In this case, the bot is receiving messages from the business connection, but it hasn't cached the associated <a href="https://corefork.telegram.org/constructor/updateBotBusinessConnect">updateBotBusinessConnect</a> with info about the connection (can it reply to messages? etc.) yet, and cannot receive the old ones because they were sent when the bot wasn't logged into the session yet.<br>
/// This method can be used to fetch info about a not-yet-cached business connection, and should not be invoked if the info is already cached or to fetch changes, as eventual changes will automatically be sent as new <a href="https://corefork.telegram.org/constructor/updateBotBusinessConnect">updateBotBusinessConnect</a> updates to the bot using the usual <a href="https://corefork.telegram.org/api/updates">update delivery methods »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CONNECTION_ID_INVALID The specified connection ID is invalid.
/// See <a href="https://corefork.telegram.org/method/account.getBotBusinessConnection" />
///</summary>
internal sealed class GetBotBusinessConnectionHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetBotBusinessConnection, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetBotBusinessConnection obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Updates = [],
            Users = [],
            Chats = [],
            Date = CurrentDate
        });
    }
}
