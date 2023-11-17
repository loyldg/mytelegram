// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns sparse positions of messages of the specified type in the chat to be used for shared media scroll implementation.Returns the results in reverse chronological order (i.e., in order of decreasing message_id).
/// See <a href="https://corefork.telegram.org/method/messages.getSearchResultsPositions" />
///</summary>
internal sealed class GetSearchResultsPositionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSearchResultsPositions, MyTelegram.Schema.Messages.ISearchResultsPositions>,
    Messages.IGetSearchResultsPositionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISearchResultsPositions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSearchResultsPositions obj)
    {
        return Task.FromResult<ISearchResultsPositions>(new TSearchResultsPositions { Count = 0, Positions = new TVector<ISearchResultsPosition>() });
    }
}
