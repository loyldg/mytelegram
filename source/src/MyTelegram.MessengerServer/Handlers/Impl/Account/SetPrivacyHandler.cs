using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SetPrivacyHandler : RpcResultObjectHandler<RequestSetPrivacy, IPrivacyRules>,
    ISetPrivacyHandler
{
    protected override Task<IPrivacyRules> HandleCoreAsync(IRequestInput input,
        RequestSetPrivacy obj)
    {
        throw new NotImplementedException();
    }
}
