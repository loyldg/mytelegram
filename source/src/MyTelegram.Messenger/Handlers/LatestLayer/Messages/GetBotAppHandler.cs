namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Obtain information about a <a href="https://corefork.telegram.org/api/bots/webapps#direct-link-mini-apps">direct link Mini App</a>
/// Possible errors
/// Code Type Description
/// 400 BOT_APP_BOT_INVALID The bot_id passed in the inputBotAppShortName constructor is invalid.
/// 400 BOT_APP_INVALID The specified bot app is invalid.
/// 400 BOT_APP_SHORTNAME_INVALID The specified bot app short name is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getBotApp"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetBotAppHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetBotApp, MyTelegram.Schema.Messages.IBotApp>
{
    protected override Task<MyTelegram.Schema.Messages.IBotApp> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetBotApp obj)
    {
        throw new NotImplementedException();
    }
}