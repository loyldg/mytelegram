namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Activates <a href="https://corefork.telegram.org/api/stories#stealth-mode">stories stealth mode</a>, see <a href="https://corefork.telegram.org/api/stories#stealth-mode">here »</a> for more info.Will return an <a href="https://corefork.telegram.org/constructor/updateStoriesStealthMode">updateStoriesStealthMode</a>.
/// Possible errors
/// Code Type Description
/// 400 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.activateStealthMode"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ActivateStealthModeHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestActivateStealthMode, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestActivateStealthMode obj)
    {
        throw new NotImplementedException();
    }
}