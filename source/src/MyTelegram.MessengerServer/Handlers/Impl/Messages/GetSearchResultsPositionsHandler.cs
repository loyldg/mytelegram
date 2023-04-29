// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetSearchResultsPositionsHandler :
    RpcResultObjectHandler<RequestGetSearchResultsPositions, ISearchResultsPositions>,
    Messages.IGetSearchResultsPositionsHandler
{
    protected override Task<ISearchResultsPositions> HandleCoreAsync(IRequestInput input,
        RequestGetSearchResultsPositions obj)
    {
        throw new NotImplementedException();
    }
}
