namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Returns installed attachment menu <a href="https://corefork.telegram.org/api/bots/attach">bot mini apps »</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getAttachMenuBots"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetAttachMenuBotsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAttachMenuBots, MyTelegram.Schema.IAttachMenuBots>
{
    protected override Task<MyTelegram.Schema.IAttachMenuBots> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetAttachMenuBots obj)
    {
        return Task.FromResult<MyTelegram.Schema.IAttachMenuBots>(new TAttachMenuBots { Users = new(), Bots = new() });
    }
}