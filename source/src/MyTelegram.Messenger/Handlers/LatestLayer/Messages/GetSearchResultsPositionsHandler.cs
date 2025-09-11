namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Returns sparse positions of messages of the specified type in the chat to be used for shared media scroll implementation.Returns the results in reverse chronological order (i.e., in order of decreasing message_id).
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getSearchResultsPositions" />
///</summary>
internal sealed class GetSearchResultsPositionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSearchResultsPositions, MyTelegram.Schema.Messages.ISearchResultsPositions>
{
    protected override Task<MyTelegram.Schema.Messages.ISearchResultsPositions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSearchResultsPositions obj)
    {
        return Task.FromResult<ISearchResultsPositions>(new TSearchResultsPositions { Count = 0, Positions = [] });
    }
}
