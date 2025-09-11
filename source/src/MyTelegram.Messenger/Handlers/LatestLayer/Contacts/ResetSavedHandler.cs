namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Removes all contacts without an associated Telegram account.
/// See <a href="https://corefork.telegram.org/method/contacts.resetSaved" />
///</summary>
internal sealed class ResetSavedHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResetSaved, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestResetSaved obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
