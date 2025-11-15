namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Get a list of bots owned by the current user
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.getAdminedBots"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetAdminedBotsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetAdminedBots, TVector<MyTelegram.Schema.IUser>>
{
    protected override Task<TVector<MyTelegram.Schema.IUser>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestGetAdminedBots obj)
    {
        throw new NotImplementedException();
    }
}