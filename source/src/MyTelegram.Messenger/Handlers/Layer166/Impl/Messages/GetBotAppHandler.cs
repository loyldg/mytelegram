// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Obtain information about a <a href="https://corefork.telegram.org/api/bots/webapps#named-bot-web-apps">named bot web app</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_APP_INVALID The specified bot app is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getBotApp" />
///</summary>
internal sealed class GetBotAppHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetBotApp, MyTelegram.Schema.Messages.IBotApp>,
    Messages.IGetBotAppHandler
{
    protected override Task<MyTelegram.Schema.Messages.IBotApp> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetBotApp obj)
    {
        throw new NotImplementedException();
    }
}
