namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Delete all temporary authorization keys <strong>except for</strong> the ones specified
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.dropTempAuthKeys"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class DropTempAuthKeysHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestDropTempAuthKeys, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestDropTempAuthKeys obj)
    {
        throw new NotImplementedException();
    }
}