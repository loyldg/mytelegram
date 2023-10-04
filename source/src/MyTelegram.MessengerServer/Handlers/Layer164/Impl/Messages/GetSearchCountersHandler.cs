using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetSearchCountersHandler : RpcResultObjectHandler<RequestGetSearchCounters, TVector<ISearchCounter>>,
    IGetSearchCountersHandler, IProcessedHandler
{
    protected override Task<TVector<ISearchCounter>> HandleCoreAsync(IRequestInput input,
        RequestGetSearchCounters obj)
    {
        //Console.WriteLine($"{GetPeer(obj.Peer, 0)}");

        //foreach (var messagesFilter in obj.Filters)
        //{
        //    Console.WriteLine($"{messagesFilter.GetType().FullName}");
        //}
        //throw new NotImplementedException();

        return Task.FromResult(new TVector<ISearchCounter>());
    }
}