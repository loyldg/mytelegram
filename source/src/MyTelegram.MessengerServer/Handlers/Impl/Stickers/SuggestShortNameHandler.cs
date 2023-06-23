using MyTelegram.Handlers.Stickers;
using MyTelegram.Schema.Stickers;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stickers;

public class SuggestShortNameHandler : RpcResultObjectHandler<RequestSuggestShortName, ISuggestedShortName>,
    ISuggestShortNameHandler
{
    protected override Task<ISuggestedShortName> HandleCoreAsync(IRequestInput input,
        RequestSuggestShortName obj)
    {
        throw new NotImplementedException();
    }
}