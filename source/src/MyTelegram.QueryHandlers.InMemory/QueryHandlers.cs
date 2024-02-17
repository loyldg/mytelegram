namespace MyTelegram.QueryHandlers.InMemory;

public static class MyQueryHandlerExtensions
{
    public static Task<T?> FirstOrDefaultAsync<T>(this IQueryable<T> query,
        Func<T, bool> predicate,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(query.FirstOrDefault(predicate));
    }

    public static Task<T> SingleAsync<T>(this IQueryable<T> query,
        Func<T, bool> predicate,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(query.Single(predicate));
    }

    public static Task<T?> SingleOrDefaultAsync<T>(this IQueryable<T> query,
        Func<T, bool> predicate,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(query.SingleOrDefault(predicate));
    }

    public static Task<List<T>> ToListAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(query.ToList());
    }
}

public class GetAllDraftQueryHandler : MyQueryHandler<DraftReadModel>,
    IQueryHandler<GetAllDraftQuery, IReadOnlyCollection<IDraftReadModel>>
{
    public GetAllDraftQueryHandler(IMyInMemoryReadStore<DraftReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IDraftReadModel>> ExecuteQueryAsync(GetAllDraftQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.OwnerPeerId == query.OwnerPeerId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetAllUserNameQueryHandler : MyQueryHandler<UserNameReadModel>,
    IQueryHandler<GetAllUserNameQuery, IReadOnlyCollection<string>>
{
    public GetAllUserNameQueryHandler(IMyInMemoryReadStore<UserNameReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<string>> ExecuteQueryAsync(GetAllUserNameQuery query,
        CancellationToken cancellationToken)
    {
        return (await CreateQueryAsync().ConfigureAwait(false))
            .OrderBy(p => p.Id)
            .Skip(query.Skip)
            .Take(query.Limit)
            .Select(p => p.UserName)
            .ToList();
    }
}

public class
    GetChannelByChannelIdListQueryHandler : MyQueryHandler<ChannelReadModel>,
        IQueryHandler<GetChannelByChannelIdListQuery,
            IReadOnlyCollection<IChannelReadModel>>
{
    public GetChannelByChannelIdListQueryHandler(IMyInMemoryReadStore<ChannelReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IChannelReadModel>> ExecuteQueryAsync(GetChannelByChannelIdListQuery query,
        CancellationToken cancellationToken)
    {
        if (query.ChannelIdList.Count == 0)
        {
            return new List<ChannelReadModel>();
        }

        // todo:pagination
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => query.ChannelIdList.Contains(p.ChannelId))
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChannelByIdQueryHandler : MyQueryHandler<ChannelReadModel>,
    IQueryHandler<GetChannelByIdQuery, IChannelReadModel>
{
    public GetChannelByIdQueryHandler(IMyInMemoryReadStore<ChannelReadModel> store) : base(store)
    {
    }

    public async Task<IChannelReadModel> ExecuteQueryAsync(GetChannelByIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = ChannelId.Create(query.ChannelId).Value;
        return await (await CreateQueryAsync().ConfigureAwait(false)).SingleAsync(p => p.Id == id, cancellationToken)
            ;
    }
}

public class GetChannelFullByIdQueryHandler : MyQueryHandler<ChannelFullReadModel>,
    IQueryHandler<GetChannelFullByIdQuery, IChannelFullReadModel?>
{
    public GetChannelFullByIdQueryHandler(IMyInMemoryReadStore<ChannelFullReadModel> store) : base(store)
    {
    }

    public async Task<IChannelFullReadModel?> ExecuteQueryAsync(GetChannelFullByIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = ChannelId.Create(query.ChannelId).Value;
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
            ;
    }
}

public class
    GetChannelIdListByMemberUidQueryHandler : MyQueryHandler<ChannelMemberReadModel>,
        IQueryHandler<GetChannelIdListByMemberUidQuery,
            IReadOnlyCollection<long>>
{
    public GetChannelIdListByMemberUidQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetChannelIdListByMemberUidQuery query,
        CancellationToken cancellationToken)
    {
        // todo:pass page size parameter
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.UserId == query.MemberUid)
                .OrderBy(p => p.Id)
                .Take(100)
                .Select(p => p.ChannelId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChannelIdListByUidQueryHandler : MyQueryHandler<ChannelMemberReadModel>,
    IQueryHandler<GetChannelIdListByUidQuery, IReadOnlyCollection<long>>
{
    public GetChannelIdListByUidQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetChannelIdListByUidQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.UserId == query.UserId)
                .Select(p => p.ChannelId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChannelMemberByUidQueryHandler : MyQueryHandler<ChannelMemberReadModel>,
    IQueryHandler<GetChannelMemberByUserIdQuery, IChannelMemberReadModel?>
{
    public GetChannelMemberByUidQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store) : base(store)
    {
    }

    public async Task<IChannelMemberReadModel?> ExecuteQueryAsync(GetChannelMemberByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = ChannelMemberId.Create(query.ChannelId, query.UserId).Value;
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
            ;
    }
}

public class GetChannelMemberListByChannelIdListQueryHandler : MyQueryHandler<ChannelMemberReadModel>, IQueryHandler<
    GetChannelMemberListByChannelIdListQuery, IReadOnlyCollection<IChannelMemberReadModel>>
{
    public GetChannelMemberListByChannelIdListQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store) :
        base(store)
    {
    }

    public async Task<IReadOnlyCollection<IChannelMemberReadModel>> ExecuteQueryAsync(
        GetChannelMemberListByChannelIdListQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.UserId == query.MemberUid && query.ChannelIdList.Contains(p.ChannelId))
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChannelMembersByChannelIdQueryHandler : MyQueryHandler<ChannelMemberReadModel>,
    IQueryHandler<GetChannelMembersByChannelIdQuery,
        IReadOnlyCollection<IChannelMemberReadModel>>
{
    public GetChannelMembersByChannelIdQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IChannelMemberReadModel>> ExecuteQueryAsync(
        GetChannelMembersByChannelIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => !p.Left && p.ChannelId == query.ChannelId)
                .WhereIf(query.MemberUidList.Count > 0, p => query.MemberUidList.Contains(p.UserId))
                .OrderBy(p => p.Id)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false)
            ;
    }
}

public class GetChannelPushUpdatesBySeqNoQueryHandler : MyQueryHandler<PushUpdatesReadModel>,
    IQueryHandler<GetChannelPushUpdatesBySeqNoQuery,
        IReadOnlyCollection<IPushUpdatesReadModel>>
{
    public GetChannelPushUpdatesBySeqNoQueryHandler(IMyInMemoryReadStore<PushUpdatesReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IPushUpdatesReadModel>> ExecuteQueryAsync(
        GetChannelPushUpdatesBySeqNoQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.SeqNo > query.SeqNo && query.ChannelIdList.Contains(p.PeerId))
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChatByChatIdListQueryHandler : MyQueryHandler<ChatReadModel>,
    IQueryHandler<GetChatByChatIdListQuery, IReadOnlyList<IChatReadModel>>
{
    public GetChatByChatIdListQueryHandler(IMyInMemoryReadStore<ChatReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyList<IChatReadModel>> ExecuteQueryAsync(GetChatByChatIdListQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => query.ChatIdList.Contains(p.ChatId))
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChatByChatIdQueryHandler : MyQueryHandler<ChatReadModel>,
    IQueryHandler<GetChatByChatIdQuery, IChatReadModel?>
{
    public GetChatByChatIdQueryHandler(IMyInMemoryReadStore<ChatReadModel> store) : base(store)
    {
    }

    public async Task<IChatReadModel?> ExecuteQueryAsync(GetChatByChatIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = ChatId.Create(query.ChatId).Value;
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken)
            ;
    }
}

public class
    GetChatInvitesQueryHandler : MyQueryHandler<ChatInviteReadModel>,
        IQueryHandler<GetChatInvitesQuery, IReadOnlyCollection<IChatInviteReadModel>>
{
    public GetChatInvitesQueryHandler(IMyInMemoryReadStore<ChatInviteReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IChatInviteReadModel>> ExecuteQueryAsync(GetChatInvitesQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p =>
                    p.Revoked == query.Revoked && p.PeerId == query.PeerId && p.AdminId == query.AdminId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetDeviceByAuthKeyIdQueryHandler : MyQueryHandler<DeviceReadModel>,
    IQueryHandler<GetDeviceByAuthKeyIdQuery, IDeviceReadModel?>
{
    public GetDeviceByAuthKeyIdQueryHandler(IMyInMemoryReadStore<DeviceReadModel> store) : base(store)
    {
    }

    public async Task<IDeviceReadModel?> ExecuteQueryAsync(GetDeviceByAuthKeyIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = DeviceId.Create(query.AuthKeyId).Value;

        return await (await CreateQueryAsync().ConfigureAwait(false))
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken)
            ;
    }
}

public class GetDeviceByHashQueryHandler : MyQueryHandler<DeviceReadModel>,
    IQueryHandler<GetDeviceByHashQuery, IDeviceReadModel?>
{
    public GetDeviceByHashQueryHandler(IMyInMemoryReadStore<DeviceReadModel> store) : base(store)
    {
    }

    public async Task<IDeviceReadModel?> ExecuteQueryAsync(GetDeviceByHashQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .FirstOrDefaultAsync(p => p.UserId == query.UserId && p.Hash == query.Hash, cancellationToken)
            ;
    }
}

public class GetDeviceByUidQueryHandler : MyQueryHandler<DeviceReadModel>,
    IQueryHandler<GetDeviceByUidQuery, IReadOnlyCollection<IDeviceReadModel>>
{
    public GetDeviceByUidQueryHandler(IMyInMemoryReadStore<DeviceReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IDeviceReadModel>> ExecuteQueryAsync(GetDeviceByUidQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.UserId == query.UserId && p.IsActive)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetDialogByIdQueryHandler : MyQueryHandler<DialogReadModel>,
    IQueryHandler<GetDialogByIdQuery, IDialogReadModel?>
{
    public GetDialogByIdQueryHandler(IMyInMemoryReadStore<DialogReadModel> store) : base(store)
    {
    }

    public async Task<IDialogReadModel?> ExecuteQueryAsync(GetDialogByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .SingleOrDefaultAsync(p => p.Id == query.Id.Value, cancellationToken)
            ;
    }
}

public class GetDialogsQueryHandler : MyQueryHandler<DialogReadModel>,
    IQueryHandler<GetDialogsQuery, IReadOnlyList<IDialogReadModel>>
{
    public GetDialogsQueryHandler(IMyInMemoryReadStore<DialogReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyList<IDialogReadModel>> ExecuteQueryAsync(GetDialogsQuery query,
        CancellationToken cancellationToken)
    {
        // Fix native aot missing metadata issues
        var needOffsetDate = false;
        var offsetDate = DateTime.UtcNow;

        if (query.OffsetDate.HasValue)
        {
            needOffsetDate = true;
            offsetDate = query.OffsetDate.Value;
        }

        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.OwnerId == query.OwnerId)
                .WhereIf(needOffsetDate, p => p.CreationTime > offsetDate)
                .WhereIf(query.Pinned.HasValue, p => p.Pinned == query.Pinned!.Value)
                .WhereIf(query.PeerIdList?.Count > 0, p => query.PeerIdList!.Contains(p.ToPeerId))
                .OrderBy(p => p.TopMessage)
                .Skip(0)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class
    GetJoinedChannelIdListQueryHandler : MyQueryHandler<ChannelMemberReadModel>,
        IQueryHandler<GetJoinedChannelIdListQuery, IReadOnlyCollection<long>>
{
    public GetJoinedChannelIdListQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetJoinedChannelIdListQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.UserId == query.MemberUid && query.ChannelIdList.Contains(p.ChannelId))
                .Select(p => p.ChannelId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetKickedChannelMembersQueryHandler : MyQueryHandler<ChannelMemberReadModel>,
    IQueryHandler<GetKickedChannelMembersQuery,
        IReadOnlyCollection<IChannelMemberReadModel>>
{
    public GetKickedChannelMembersQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IChannelMemberReadModel>> ExecuteQueryAsync(
        GetKickedChannelMembersQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.ChannelId == query.ChannelId && p.Kicked)
                .OrderBy(p => p.Id)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class
    GetMegaGroupByUidQueryHandler : MyQueryHandler<ChannelReadModel>,
        IQueryHandler<GetMegaGroupByUidQuery, IReadOnlyCollection<IChannelReadModel>>
{
    public GetMegaGroupByUidQueryHandler(IMyInMemoryReadStore<ChannelReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IChannelReadModel>> ExecuteQueryAsync(GetMegaGroupByUidQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.MegaGroup && p.CreatorId == query.UserId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetMessageByIdQueryHandler : MyQueryHandler<MessageReadModel>,
    IQueryHandler<GetMessageByIdQuery, IMessageReadModel?>
{
    public GetMessageByIdQueryHandler(IMyInMemoryReadStore<MessageReadModel> store) : base(store)
    {
    }

    public async Task<IMessageReadModel?> ExecuteQueryAsync(GetMessageByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);
    }
}

public class GetMessageIdListQueryHandler : MyQueryHandler<MessageReadModel>,
    IQueryHandler<GetMessageIdListQuery, List<int>>
{
    public GetMessageIdListQueryHandler(IMyInMemoryReadStore<MessageReadModel> store) : base(store)
    {
    }

    public async Task<List<int>> ExecuteQueryAsync(GetMessageIdListQuery query,
        CancellationToken cancellationToken)
    {
        var maxId = query.MaxMessageId;
        if (maxId == 0)
        {
            maxId = int.MaxValue;
        }

        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p =>
                    p.OwnerPeerId == query.OwnerPeerId && p.ToPeerId == query.ToPeerId && p.MessageId < maxId)
                .Select(p => p.MessageId)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class
    GetMessageReadHistoryQueryHandler : MyQueryHandler<ReadingHistoryReadModel>,
        IQueryHandler<GetReadingHistoryQuery, IReadOnlyCollection<long>>
{
    public GetMessageReadHistoryQueryHandler(IMyInMemoryReadStore<ReadingHistoryReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetReadingHistoryQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.TargetPeerId == query.TargetPeerId && p.MessageId == query.MessageId)
                .OrderBy(p => p.Id)
                // todo:set limit
                .Take(200)
                .Select(p => p.TargetPeerId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetMessagesByMessageIdListQueryHandler : MyQueryHandler<MessageReadModel>,
    IQueryHandler<GetMessagesByMessageIdListQuery,
        IReadOnlyCollection<IMessageReadModel>>
{
    public GetMessagesByMessageIdListQueryHandler(IMyInMemoryReadStore<MessageReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(
        GetMessagesByMessageIdListQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => query.MessageIdList.Contains(p.MessageId))
                .ToListAsync(cancellationToken)
            ;
    }
}

public class
    GetMessagesByUserQueryHandler : MyQueryHandler<MessageReadModel>, IQueryHandler<GetMessagesByUserIdQuery,
        IReadOnlyCollection<IMessageReadModel>>
{
    public GetMessagesByUserQueryHandler(IMyInMemoryReadStore<MessageReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(GetMessagesByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.OwnerPeerId == query.OwnerPeerId && p.ToPeerId == query.ToPeerId)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false)
            ;
    }
}

public class
    GetMessagesQueryHandler : MyQueryHandler<MessageReadModel>,
        IQueryHandler<GetMessagesQuery, IReadOnlyCollection<IMessageReadModel>>
{
    public GetMessagesQueryHandler(IMyInMemoryReadStore<MessageReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(GetMessagesQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.OwnerPeerId == query.OwnerPeerId)
                .WhereIf(query.Q?.Length > 2, p => p.Message.Contains(query.Q!))
                .WhereIf(
                    query.MessageType != MessageType.Unknown && query.MessageType != MessageType.Pinned,
                    p => p.MessageType == query.MessageType)
                .WhereIf(query.MessageType == MessageType.Pinned, p => p.Pinned)
                .WhereIf(query.MessageIdList?.Count > 0, p => query.MessageIdList!.Contains(p.MessageId))
                .WhereIf(query.ChannelHistoryMinId > 0, p => p.MessageId > query.ChannelHistoryMinId)
                .WhereIf(query.Offset?.LoadType == LoadType.Forward, p => p.MessageId > query.Offset!.FromId)
                .WhereIf(query.Offset?.MaxId > 0, p => p.MessageId < query.Offset!.MaxId)
                .WhereIf(query.Pts > 0, p => p.Pts > query.Pts)
                .WhereIf(query.Peer != null,
                    p => p.ToPeerType == query.Peer!.PeerType && p.ToPeerId == query.Peer.PeerId)
                .OrderByDescending(p => p.MessageId)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetMessageViewsQueryHandler : MyQueryHandler<MessageReadModel>,
    IQueryHandler<GetMessageViewsQuery, IReadOnlyCollection<MessageView>>
{
    public GetMessageViewsQueryHandler(IMyInMemoryReadStore<MessageReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<MessageView>> ExecuteQueryAsync(GetMessageViewsQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.OwnerPeerId == query.ChannelId && query.MessageIdList.Contains(p.MessageId))
                .Select(p => new MessageView { MessageId = p.MessageId, Views = p.Views ?? 0 })
                .ToListAsync(cancellationToken)
            ;
    }
}

public class
    GetPeerNotifySettingsByIdQueryHandler : MyQueryHandler<PeerNotifySettingsReadModel>,
        IQueryHandler<GetPeerNotifySettingsByIdQuery,
            IPeerNotifySettingsReadModel>
{
    public GetPeerNotifySettingsByIdQueryHandler(IMyInMemoryReadStore<PeerNotifySettingsReadModel> store) : base(store)
    {
    }

    public async Task<IPeerNotifySettingsReadModel> ExecuteQueryAsync(GetPeerNotifySettingsByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefaultAsync(p => p.Id == query.Id.Value, cancellationToken)
            .ConfigureAwait(false) ?? new PeerNotifySettingsReadModel();
    }
}

public class GetPeerNotifySettingsListQueryHandler : MyQueryHandler<PeerNotifySettingsReadModel>,
    IQueryHandler<GetPeerNotifySettingsListQuery,
        IReadOnlyCollection<IPeerNotifySettingsReadModel>>
{
    public GetPeerNotifySettingsListQueryHandler(IMyInMemoryReadStore<PeerNotifySettingsReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IPeerNotifySettingsReadModel>> ExecuteQueryAsync(
        GetPeerNotifySettingsListQuery query,
        CancellationToken cancellationToken)
    {
        var peerIdList = query.PeerNotifySettingsIdList.Select(p => p.Value).ToList();

        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => peerIdList.Contains(p.Id))
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetPtsByPeerIdQueryHandler : MyQueryHandler<PtsReadModel>,
    IQueryHandler<GetPtsByPeerIdQuery, IPtsReadModel?>
{
    public GetPtsByPeerIdQueryHandler(IMyInMemoryReadStore<PtsReadModel> store) : base(store)
    {
    }

    public async Task<IPtsReadModel?> ExecuteQueryAsync(GetPtsByPeerIdQuery query,
        CancellationToken cancellationToken)
    {
        var ptsId = PtsId.Create(query.PeerId).Value;
        return await (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefaultAsync(p => p.Id == ptsId, cancellationToken);
    }
}

public class GetPtsByPermAuthKeyIdQueryHandler : MyQueryHandler<PtsForAuthKeyIdReadModel>,
    IQueryHandler<GetPtsByPermAuthKeyIdQuery, IPtsForAuthKeyIdReadModel?>
{
    public GetPtsByPermAuthKeyIdQueryHandler(IMyInMemoryReadStore<PtsForAuthKeyIdReadModel> store) : base(store)
    {
    }

    public async Task<IPtsForAuthKeyIdReadModel?> ExecuteQueryAsync(GetPtsByPermAuthKeyIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = PtsId.Create(query.PeerId, query.PermAuthKeyId);
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .FirstOrDefaultAsync(p => p.Id == id.Value, cancellationToken)
            ;
    }
}

public class
    GetPushUpdatesByPtsQueryHandler : MyQueryHandler<PushUpdatesReadModel>, IQueryHandler<GetPushUpdatesByPtsQuery,
        IReadOnlyCollection<IPushUpdatesReadModel>>
{
    public GetPushUpdatesByPtsQueryHandler(IMyInMemoryReadStore<PushUpdatesReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IPushUpdatesReadModel>> ExecuteQueryAsync(GetPushUpdatesByPtsQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.PeerId == query.PeerId && p.Pts > query.Pts)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetPushUpdatesQueryHandler : MyQueryHandler<PushUpdatesReadModel>,
    IQueryHandler<GetPushUpdatesQuery, IReadOnlyCollection<IPushUpdatesReadModel>>
{
    public GetPushUpdatesQueryHandler(IMyInMemoryReadStore<PushUpdatesReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IPushUpdatesReadModel>> ExecuteQueryAsync(GetPushUpdatesQuery query,
        CancellationToken cancellationToken)
    {
        return (await CreateQueryAsync().ConfigureAwait(false))
            .Where(p => p.PeerId == query.PeerId && p.Pts > query.MinPts)
            .Take(query.Limit)
            .ToList();
    }
}

public class
    GetReadHistoryMessageQueryHandler : MyQueryHandler<MessageReadModel>,
        IQueryHandler<GetReadHistoryMessageQuery, IMessageReadModel?>
{
    public GetReadHistoryMessageQueryHandler(IMyInMemoryReadStore<MessageReadModel> store) : base(store)
    {
    }

    public async Task<IMessageReadModel?> ExecuteQueryAsync(GetReadHistoryMessageQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .SingleOrDefaultAsync(p =>
                        p.OwnerPeerId == query.OwnerPeerId && p.MessageId == query.MessageId &&
                        p.ToPeerId == query.ToPeerId,
                    cancellationToken)
            ;
    }
}

public class GetRpcResultByIdQueryHandler : MyQueryHandler<RpcResultReadModel>,
    IQueryHandler<GetRpcResultByIdQuery, IRpcResultReadModel?>
{
    public GetRpcResultByIdQueryHandler(IMyInMemoryReadStore<RpcResultReadModel> store) : base(store)
    {
    }

    public async Task<IRpcResultReadModel?> ExecuteQueryAsync(GetRpcResultByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);
    }
}

public class GetUserByIdQueryHandler : MyQueryHandler<UserReadModel>, IQueryHandler<GetUserByIdQuery, IUserReadModel?>
{
    public GetUserByIdQueryHandler(IMyInMemoryReadStore<UserReadModel> store) : base(store)
    {
    }

    public async Task<IUserReadModel?> ExecuteQueryAsync(GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        return (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefault(p => p.Id == UserId.Create(query.UserId).Value)
            ;
    }
}

public class GetUserByPhoneNumberQueryHandler : MyQueryHandler<UserReadModel>,
    IQueryHandler<GetUserByPhoneNumberQuery, IUserReadModel?>
{
    public GetUserByPhoneNumberQueryHandler(IMyInMemoryReadStore<UserReadModel> store) : base(store)
    {
    }

    public async Task<IUserReadModel?> ExecuteQueryAsync(GetUserByPhoneNumberQuery query,
        CancellationToken cancellationToken)
    {
        return (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefault(p => p.PhoneNumber == query.PhoneNumber)
            ;
    }
}

public class GetUserNameByIdQueryHandler : MyQueryHandler<UserNameReadModel>,
    IQueryHandler<GetUserNameByIdQuery, IUserNameReadModel?>
{
    public GetUserNameByIdQueryHandler(IMyInMemoryReadStore<UserNameReadModel> store) : base(store)
    {
    }

    public async Task<IUserNameReadModel?> ExecuteQueryAsync(GetUserNameByIdQuery query,
        CancellationToken cancellationToken)
    {
        return (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefault(p => p.Id == UserNameId.Create(query.UserName).Value)
            ;
    }
}

public class
    GetUserNameByNameQueryHandler : IQueryHandler<GetUserNameByNameQuery, IUserNameReadModel?>
{
    private readonly IMyInMemoryReadStore<UserNameReadModel> _store;

    public GetUserNameByNameQueryHandler(IMyInMemoryReadStore<UserNameReadModel> store)
    {
        _store = store;
    }

    public async Task<IUserNameReadModel?> ExecuteQueryAsync(GetUserNameByNameQuery query,
        CancellationToken cancellationToken)
    {
        var db = await _store.AsQueryable(cancellationToken);
        return db
                .FirstOrDefault(p => p.Id == UserNameId.Create(query.Name).Value)
            ;
    }
}

public class
    GetUsersByUidListQueryHandler : MyQueryHandler<UserReadModel>,
        IQueryHandler<GetUsersByUidListQuery, IReadOnlyList<IUserReadModel>>
{
    public GetUsersByUidListQueryHandler(IMyInMemoryReadStore<UserReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyList<IUserReadModel>> ExecuteQueryAsync(GetUsersByUidListQuery query,
        CancellationToken cancellationToken)
    {
        if (query.UidList.Count == 0)
        {
            return new List<UserReadModel>();
        }

        return (await CreateQueryAsync().ConfigureAwait(false))
            .Where(p => query.UidList.Contains(p.UserId))
            .ToList();
    }
}

public class MyQueryHandler<TReadModel> where TReadModel : class, IReadModel
{
    private readonly IMyInMemoryReadStore<TReadModel> _store;

    public MyQueryHandler(IMyInMemoryReadStore<TReadModel> store)
    {
        _store = store;
    }

    public Task<IQueryable<TReadModel>> CreateQueryAsync()
    {
        return _store.AsQueryable();
    }
}

public class
    SearchUserByKeywordQueryHandler : MyQueryHandler<UserReadModel>,
        IQueryHandler<SearchUserByKeywordQuery, IReadOnlyCollection<IUserReadModel>>
{
    public SearchUserByKeywordQueryHandler(IMyInMemoryReadStore<UserReadModel> store) : base(store)
    {
    }

    public async Task<IReadOnlyCollection<IUserReadModel>> ExecuteQueryAsync(SearchUserByKeywordQuery query,
        CancellationToken cancellationToken)
    {
        var q = query.Keyword;
        if (!string.IsNullOrEmpty(q) && q.StartsWith("@"))
        {
            q = query.Keyword[1..];
        }

        return (await CreateQueryAsync().ConfigureAwait(false))
            .WhereIf(!string.IsNullOrEmpty(q),
                p => (p.UserName != null && p.UserName.StartsWith(q)) || p.FirstName.Contains(q))
            .OrderBy(p => p.UserName)
            .Take(50)
            .ToList();
    }
}

public class SearchUserNameQueryHandler : IQueryHandler<SearchUserNameQuery, IReadOnlyCollection<IUserNameReadModel>>
{
    private readonly IMyInMemoryReadStore<UserNameReadModel> _store;

    public SearchUserNameQueryHandler(IMyInMemoryReadStore<UserNameReadModel> store)
    {
        _store = store;
    }

    public async Task<IReadOnlyCollection<IUserNameReadModel>> ExecuteQueryAsync(SearchUserNameQuery query,
        CancellationToken cancellationToken)
    {
        var db = await _store.AsQueryable(cancellationToken);
        return db
                .Where(p => p.UserName.StartsWith(query.Keyword))
                //.OrderBy(p => p.Id)
                .Take(50)
                .ToList()
            ;
    }
}

public class GetLatestAppCodeQueryHandler : MyQueryHandler<AppCodeReadModel>,
    IQueryHandler<GetLatestAppCodeQuery, IAppCodeReadModel>
{
    public GetLatestAppCodeQueryHandler(IMyInMemoryReadStore<AppCodeReadModel> store) : base(store)
    {
    }

    public async Task<IAppCodeReadModel> ExecuteQueryAsync(GetLatestAppCodeQuery query,
        CancellationToken cancellationToken)
    {
        var item = await (await CreateQueryAsync().ConfigureAwait(false)).FirstOrDefaultAsync(p =>
                        p.PhoneNumber == query.PhoneNumber && p.PhoneCodeHash == query.PhoneCodeHash,
                    cancellationToken)
                .ConfigureAwait(false)
            ;
        return item ?? new AppCodeReadModel();
    }
}
