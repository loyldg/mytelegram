namespace MyTelegram.Domain.Events.AppCode;

public class SignUpRequiredEvent : AggregateEvent<AppCodeAggregate, AppCodeId>
{
    public SignUpRequiredEvent(long reqMsgId)
    {
        ReqMsgId = reqMsgId;
    }

    public long ReqMsgId { get; }
}
