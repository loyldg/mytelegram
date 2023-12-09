// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns attachment menu entry for a <a href="https://corefork.telegram.org/api/bots/attach">bot web app that can be launched from the attachment menu »</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// See <a href="https://corefork.telegram.org/method/messages.getAttachMenuBot" />
///</summary>
internal sealed class GetAttachMenuBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAttachMenuBot, MyTelegram.Schema.IAttachMenuBotsBot>,
    Messages.IGetAttachMenuBotHandler
{
    protected override Task<MyTelegram.Schema.IAttachMenuBotsBot> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetAttachMenuBot obj)
    {
        throw new NotImplementedException();
    }
}
