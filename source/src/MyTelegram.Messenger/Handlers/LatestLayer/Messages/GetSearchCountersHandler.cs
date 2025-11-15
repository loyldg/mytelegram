namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get the number of results that would be found by a <a href="https://corefork.telegram.org/method/messages.search">messages.search</a> call with the same parameters
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getSearchCounters"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSearchCountersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSearchCounters, TVector<MyTelegram.Schema.Messages.ISearchCounter>>
{
    protected override Task<TVector<ISearchCounter>> HandleCoreAsync(IRequestInput input, RequestGetSearchCounters obj)
    {
        return Task.FromResult(new TVector<ISearchCounter>(obj.Filters.Select(p => new TSearchCounter { Count = 0, Filter = p })));
    }
}