// ReSharper disable All

using MyTelegram.Schema.Contacts;

namespace MyTelegram.Handlers.Contacts;

public class ResolvePhoneHandler : RpcResultObjectHandler<RequestResolvePhone, IResolvedPeer>,
    Contacts.IResolvePhoneHandler
{
    protected override Task<IResolvedPeer> HandleCoreAsync(IRequestInput input,
        RequestResolvePhone obj)
    {
        throw new NotImplementedException();
    }
}