namespace MyTelegram.Domain.Events;

public abstract class RequestAggregateEvent<TAggregate, TIdentity> : AggregateEvent<TAggregate, TIdentity>,
    IHasRequestMessageId
    where TIdentity : IIdentity where TAggregate : IAggregateRoot<TIdentity>
{
    protected RequestAggregateEvent(long reqMsgId)
    {
        ReqMsgId = reqMsgId;
    }

    public long ReqMsgId { get; }
}

//public abstract class RpcAggregateEvent<TAggregate, TIdentity> : AggregateEvent<TAggregate, TIdentity> where TIdentity : IIdentity where TAggregate : IAggregateRoot<TIdentity>
//{
//    protected RpcAggregateEvent(long authKeyId,
//        long reqMsgId,
//        int reqSeqNumber)
//    {
//        AuthKeyId = authKeyId;
//        ReqMsgId = reqMsgId;
//        ReqSeqNumber = reqSeqNumber;
//    }

//    public long AuthKeyId { get; }
//    public long ReqMsgId { get; }
//    public int ReqSeqNumber { get; }
//}
