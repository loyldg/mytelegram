namespace MyTelegram.Domain.Events.UserName;

public class UserNameDeletedEvent : AggregateEvent<UserNameAggregate, UserNameId>
{
}

//public class UserNameCheckCompletedEvent:RequestAggregateEvent<UserNameAggregate,UserNameId>
//{
//    public UserNameCheckCompletedEvent(long reqMsgId,
//        bool isOk) : base(reqMsgId)
//    {
//        IsOk = isOk;
//    }

//    public bool IsOk { get; }
//}
