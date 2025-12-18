namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Verify a user or chat <a href="https://corefork.telegram.org/api/bots/verification">on behalf of an organization »</a>.
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 403 BOT_VERIFIER_FORBIDDEN This bot cannot assign <a href="https://corefork.telegram.org/api/bots/verification">verification icons</a>.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.setCustomVerification"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SetCustomVerificationHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetCustomVerification, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestSetCustomVerification obj)
    {
        throw new NotImplementedException();
    }
}