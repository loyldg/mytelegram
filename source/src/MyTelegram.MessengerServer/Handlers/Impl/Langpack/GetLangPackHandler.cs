using MyTelegram.Handlers.Langpack;
using MyTelegram.Schema.Langpack;

namespace MyTelegram.MessengerServer.Handlers.Impl.Langpack;

public class GetLangPackHandler : RpcResultObjectHandler<RequestGetLangPack, ILangPackDifference>,
    IGetLangPackHandler, IProcessedHandler
{
    protected override Task<ILangPackDifference> HandleCoreAsync(IRequestInput input,
        RequestGetLangPack obj)
    {
        ILangPackDifference r = new TLangPackDifference {
            FromVersion = 0, LangCode = obj.LangCode, Strings = new TVector<ILangPackString>(), Version = 0
        };
        return Task.FromResult(r);
    }
}

public class GetLangPackHandlerLayerN : RpcResultObjectHandler<Schema.LayerN.RequestGetLangPack, ILangPackDifference>,
    IGetDifferenceHandlerLayerN, IProcessedHandler
{
    protected override Task<ILangPackDifference> HandleCoreAsync(IRequestInput input,
        Schema.LayerN.RequestGetLangPack obj)
    {
        ILangPackDifference r = new TLangPackDifference {
            FromVersion = 0, LangCode = obj.LangCode, Strings = new TVector<ILangPackString>(), Version = 0
        };
        return Task.FromResult(r);
    }
}
