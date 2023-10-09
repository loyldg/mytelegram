namespace MyTelegram.Domain;

public interface IHasRequestMessageId
{
    long ReqMsgId { get; }
}


public interface IHasRequestInfo : IHasCorrelationId
{
    RequestInfo RequestInfo { get; }

    Guid IHasCorrelationId.CorrelationId => RequestInfo.RequestId;
}