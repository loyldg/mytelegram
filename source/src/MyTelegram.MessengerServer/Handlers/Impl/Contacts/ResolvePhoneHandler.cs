// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

public class ResolvePhoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResolvePhone, MyTelegram.Schema.Contacts.IResolvedPeer>,
    Contacts.IResolvePhoneHandler
{
    protected override Task<MyTelegram.Schema.Contacts.IResolvedPeer> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestResolvePhone obj)
    {
        throw new NotImplementedException();
    }
}
