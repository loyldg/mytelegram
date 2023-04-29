using MyTelegram.Handlers.Langpack;
using MyTelegram.Schema.Langpack;

namespace MyTelegram.MessengerServer.Handlers.Impl.Langpack;

public class GetDifferenceHandler : RpcResultObjectHandler<RequestGetDifference, ILangPackDifference>,
    IGetDifferenceHandler, IProcessedHandler
{
    protected override Task<ILangPackDifference> HandleCoreAsync(IRequestInput input,
        RequestGetDifference obj)
    {
        ILangPackDifference r = new TLangPackDifference
        {
            FromVersion = 0,
            LangCode = obj.LangCode,
            Strings = new TVector<ILangPackString>(),
            //Strings = new TVector<ILangPackString>
            //{
            //    new TLangPackString{Key = "lng_about_version",Value = "version-x- {version}"}
            //},
            Version = 0
        };
        return Task.FromResult(r);
    }
}
