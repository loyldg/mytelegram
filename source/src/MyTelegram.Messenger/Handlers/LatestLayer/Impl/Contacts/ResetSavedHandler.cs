// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Delete saved contacts
/// See <a href="https://corefork.telegram.org/method/contacts.resetSaved" />
///</summary>
internal sealed class ResetSavedHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResetSaved, IBool>,
    Contacts.IResetSavedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestResetSaved obj)
    {
        throw new NotImplementedException();
    }
}
