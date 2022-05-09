namespace MyTelegram.Domain;

public interface IHasRequestMessageId
{
    long ReqMsgId { get; }
}

public interface IHasRequestInfo
{
    RequestInfo Request { get; }
}