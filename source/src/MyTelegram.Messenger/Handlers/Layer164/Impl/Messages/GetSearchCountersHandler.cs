// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get the number of results that would be found by a <a href="https://corefork.telegram.org/method/messages.search">messages.search</a> call with the same parameters
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getSearchCounters" />
///</summary>
internal sealed class GetSearchCountersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSearchCounters, TVector<MyTelegram.Schema.Messages.ISearchCounter>>,
    Messages.IGetSearchCountersHandler
{
    protected override Task<TVector<MyTelegram.Schema.Messages.ISearchCounter>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSearchCounters obj)
    {
        throw new NotImplementedException();
    }
}
