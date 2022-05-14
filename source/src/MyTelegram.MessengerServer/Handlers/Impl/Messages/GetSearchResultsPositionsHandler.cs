// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class GetSearchResultsPositionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSearchResultsPositions, MyTelegram.Schema.Messages.ISearchResultsPositions>,
    Messages.IGetSearchResultsPositionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISearchResultsPositions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSearchResultsPositions obj)
    {
        throw new NotImplementedException();
    }
}
