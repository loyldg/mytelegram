using MyTelegram.Schema.Updates;

namespace MyTelegram.Messenger.TLObjectConverters.Mappers;

public class CustomObjectMapper : ILayeredMapper,
    IObjectMapper<IPtsReadModel, TState>,
    IObjectMapper<SearchGlobalInput, GetMessagesQuery>,
    IObjectMapper<SearchInput, GetMessagesQuery>,
    IObjectMapper<GetHistoryInput, GetMessagesQuery>,
    IObjectMapper<GetMessagesInput, GetMessagesQuery>,
    IObjectMapper<GetRepliesInput, GetMessagesQuery>,
                                                  ITransientDependency
{
    public GetMessagesQuery Map(GetHistoryInput source)
    {
        return Map(source, null!);
    }

    public GetMessagesQuery Map(GetHistoryInput source,
        GetMessagesQuery destination)
    {
        return new GetMessagesQuery(source.OwnerPeerId,
            MessageType.Unknown,
            null,
            [],
            source.ChannelHistoryMinId,
            source.Limit,
            null,
            source.Peer,
            source.SelfUserId,
            0);
    }

    public GetMessagesQuery Map(GetMessagesInput source)
    {
        return Map(source, null!);
    }

    public GetMessagesQuery Map(GetMessagesInput source,
        GetMessagesQuery destination)
    {
        return new GetMessagesQuery(source.OwnerPeerId,
            MessageType.Unknown,
            null,
            source.MessageIdList,
            0,
            source.Limit,
            null,
            source.Peer,
            source.SelfUserId,
            0);
    }

    public GetMessagesQuery? Map(GetRepliesInput source)
    {
        return Map(source, null!);
    }

    public GetMessagesQuery? Map(GetRepliesInput source,
        GetMessagesQuery destination)
    {
        return new GetMessagesQuery(source.OwnerPeerId,
            MessageType.Unknown,
            null,
            [],
            0,
            source.Limit,
            null,
            null,
            source.SelfUserId,
            0,
            source.ReplyToMsgId);
    }

    public TState Map(IPtsReadModel source)
    {
        return Map(source, new TState());
    }

    public TState Map(IPtsReadModel source,
        TState destination)
    {
        destination.Date = source.Date;
        destination.Pts = source.Pts;
        destination.Qts = source.Qts;
        destination.UnreadCount = source.UnreadCount;

        return destination;
    }

    public GetMessagesQuery Map(SearchGlobalInput source)
    {
        return Map(source, null!);
    }

    public GetMessagesQuery Map(SearchGlobalInput source,
        GetMessagesQuery destination)
    {
        return new GetMessagesQuery(source.OwnerPeerId,
                source.MessageType,
                source.Q,
                [],
                0,
                source.Limit,
                null,
                null,
                source.SelfUserId,
                0,
                BroadcastsOnly: source.BroadcastsOnly,
                GroupsOnly: source.GroupsOnly,
                UsersOnly: source.UsersOnly,
                Tokens: source.Tokens
            )
        {
            IsSearchGlobal = source.IsSearchGlobal,
            JoinedChannelIdList = source.JoinedChannelList
        };
    }

    public GetMessagesQuery Map(SearchInput source)
    {
        return Map(source, null!);
    }

    public GetMessagesQuery Map(SearchInput source,
        GetMessagesQuery destination)
    {
        return new GetMessagesQuery(source.OwnerPeerId,
            source.MessageType,
            source.Q,
            [],
            0,
            source.Limit,
            null,
            source.Peer,
            source.SelfUserId,
            0,
            Tokens: source.Tokens
            );
    }
}