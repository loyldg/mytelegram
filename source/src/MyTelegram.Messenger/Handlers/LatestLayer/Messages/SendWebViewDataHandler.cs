namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Used by the user to relay data from an opened <a href="https://corefork.telegram.org/api/bots/webapps">reply keyboard bot mini app</a> to the bot that owns it.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// See <a href="https://corefork.telegram.org/method/messages.sendWebViewData" />
///</summary>
internal sealed class SendWebViewDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendWebViewData, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendWebViewData obj)
    {
        throw new NotImplementedException();
    }
}
