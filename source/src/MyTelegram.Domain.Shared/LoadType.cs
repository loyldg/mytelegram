// ReSharper disable once CheckNamespace

namespace MyTelegram;

public enum LoadType
{
    Backward,
    Forward,
    FirstUnread,
    AroundMessage,
    AroundDate,
    LimitIs1
}

public enum ReactionType
{
    ReactionNone,
    ReactionAll,
    ReactionSome,
}
