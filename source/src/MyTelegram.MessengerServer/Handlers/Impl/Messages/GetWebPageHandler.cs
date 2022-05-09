using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetWebPageHandler : RpcResultObjectHandler<RequestGetWebPage, IWebPage>,
    IGetWebPageHandler, IProcessedHandler
{
    protected override Task<IWebPage> HandleCoreAsync(IRequestInput input,
        RequestGetWebPage obj)
    {
        // TODO:get web page
        return Task.FromResult<IWebPage>(new TWebPageEmpty());
    }
}
