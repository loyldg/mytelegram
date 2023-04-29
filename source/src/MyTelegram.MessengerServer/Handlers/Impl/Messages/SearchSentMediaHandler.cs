// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class SearchSentMediaHandler : RpcResultObjectHandler<RequestSearchSentMedia, IMessages>,
    Messages.ISearchSentMediaHandler
{
    protected override Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestSearchSentMedia obj)
    {
        throw new NotImplementedException();
    }
}
