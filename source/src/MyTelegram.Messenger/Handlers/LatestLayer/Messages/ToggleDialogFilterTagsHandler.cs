namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Enable or disable <a href="https://corefork.telegram.org/api/folders#folder-tags">folder tags »</a>.
/// Possible errors
/// Code Type Description
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.toggleDialogFilterTags"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleDialogFilterTagsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleDialogFilterTags, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestToggleDialogFilterTags obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}