namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Enable or disable <a href="https://corefork.telegram.org/api/folders#folder-tags">folder tags »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// See <a href="https://corefork.telegram.org/method/messages.toggleDialogFilterTags" />
///</summary>
internal sealed class ToggleDialogFilterTagsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleDialogFilterTags, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleDialogFilterTags obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
