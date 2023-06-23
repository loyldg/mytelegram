using MyTelegram.Handlers.Langpack;
using MyTelegram.Schema.Langpack;

namespace MyTelegram.MessengerServer.Handlers.Impl.Langpack;

public class GetStringsHandler : RpcResultObjectHandler<RequestGetStrings, TVector<ILangPackString>>,
    IGetStringsHandler, IProcessedHandler
{
    protected override Task<TVector<ILangPackString>> HandleCoreAsync(IRequestInput input,
        RequestGetStrings obj)
    {
        var r = new TVector<ILangPackString>();

        return Task.FromResult(r);
    }
}