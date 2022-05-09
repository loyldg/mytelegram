using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetPrivacyHandler : RpcResultObjectHandler<RequestGetPrivacy, IPrivacyRules>,
    IGetPrivacyHandler
{
    protected override Task<IPrivacyRules> HandleCoreAsync(IRequestInput input,
        RequestGetPrivacy obj)
    {
        throw new NotImplementedException();
    }
}
