namespace MyTelegram.Messenger.Handlers.LatestLayer.Smsjobs;
/// <summary>
/// Enable SMS jobs (official clients only).
/// <para><c>See <a href="https://corefork.telegram.org/method/smsjobs.join"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class JoinHandler : RpcResultObjectHandler<MyTelegram.Schema.Smsjobs.RequestJoin, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Smsjobs.RequestJoin obj)
    {
        throw new NotImplementedException();
    }
}