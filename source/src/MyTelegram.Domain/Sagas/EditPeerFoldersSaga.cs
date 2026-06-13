namespace MyTelegram.Domain.Sagas;

public class EditPeerFoldersSaga : MyInMemoryAggregateSaga<EditPeerFoldersSaga, EditPeerFoldersSagaId, EditPeerFoldersSagaLocator>,
    ISagaIsStartedBy<TempAggregate, TempId, EditPeerFoldersStartedEvent>,
    ISagaHandles<DialogAggregate, DialogId, DialogFolderUpdatedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly EditPeerFoldersSagaState _state = new();
    public EditPeerFoldersSaga(EditPeerFoldersSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public Task HandleAsync(IDomainEvent<TempAggregate, TempId, EditPeerFoldersStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var folderPeers = domainEvent.AggregateEvent.FolderPeers.ToList();
        var totalCount = folderPeers.Count(p =>
        {
            return p.Peer switch
            {
                TInputPeerChannel or TInputPeerChat or TInputPeerSelf or TInputPeerUser => true,
                _ => false
            };
        });

        Emit(new EditPeerFoldersStartedSagaEvent(domainEvent.AggregateEvent.RequestInfo, totalCount));
        foreach (var item in folderPeers)
        {
            Peer? toPeer = null;
            switch (item.Peer)
            {
                case TInputPeerChannel inputPeerChannel:
                    toPeer = new Peer(PeerType.Channel, inputPeerChannel.ChannelId);
                    break;
                case TInputPeerChat inputPeerChat:
                    toPeer = new Peer(PeerType.Chat, inputPeerChat.ChatId);
                    break;
                case TInputPeerSelf:
                    toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.RequestInfo.UserId);
                    break;
                case TInputPeerUser inputPeerUser:
                    toPeer = new Peer(PeerType.User, inputPeerUser.UserId);
                    break;
            }

            if (toPeer != null)
            {
                Emit(new EditPeerFoldersPeerFolderUpdatedSagaEvent(new FolderPeer(toPeer, item.FolderId)));

                var command = new UpdateDialogFolderCommand(
                    DialogId.Create(domainEvent.AggregateEvent.RequestInfo.UserId, toPeer),
                    domainEvent.AggregateEvent.RequestInfo,
                    item.FolderId
                );
                Publish(command);
            }
        }

        return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, DialogFolderUpdatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, domainEvent.AggregateEvent.OwnerUserId, cancellationToken: cancellationToken);
        Emit(new EditPeerFoldersPtsIncrementedSagaEvent(pts));
        await HandleEditPeerFoldersCompletedAsync();
    }

    private Task HandleEditPeerFoldersCompletedAsync()
    {
        if (_state.PtsCount == _state.TotalCount)
        {
            Emit(new EditPeerFoldersCompletedSagaEvent(_state.RequestInfo, _state.Pts, _state.PtsCount, _state.FolderPeers));
            return CompleteAsync();
        }

        return Task.CompletedTask;
    }
}

public class EditPeerFoldersPtsIncrementedSagaEvent(int pts) : AggregateEvent<EditPeerFoldersSaga, EditPeerFoldersSagaId>
{
    public int Pts { get; } = pts;
}

public class EditPeerFoldersStartedSagaEvent(RequestInfo requestInfo, int totalCount) : AggregateEvent<EditPeerFoldersSaga, EditPeerFoldersSagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public int TotalCount { get; } = totalCount;
}


public class EditPeerFoldersPeerFolderUpdatedSagaEvent(FolderPeer peer) : AggregateEvent<EditPeerFoldersSaga, EditPeerFoldersSagaId>
{
    public FolderPeer Peer { get; } = peer;
}

public class EditPeerFoldersCompletedSagaEvent(RequestInfo requestInfo, int pts, int ptsCount, List<FolderPeer> folderPeers) : AggregateEvent<EditPeerFoldersSaga, EditPeerFoldersSagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public int Pts { get; } = pts;
    public int PtsCount { get; } = ptsCount;
    public List<FolderPeer> FolderPeers { get; } = folderPeers;
}

public class
    EditPeerFoldersSagaState : AggregateState<EditPeerFoldersSaga, EditPeerFoldersSagaId, EditPeerFoldersSagaState>,
    IApply<EditPeerFoldersStartedSagaEvent>,
    IApply<EditPeerFoldersPeerFolderUpdatedSagaEvent>,
    IApply<EditPeerFoldersPtsIncrementedSagaEvent>,
    IApply<EditPeerFoldersCompletedSagaEvent>
{
    public RequestInfo RequestInfo { get; private set; } = default!;
    public int Pts { get; private set; }
    public int PtsCount { get; private set; }
    public int TotalCount { get; private set; }
    public List<FolderPeer> FolderPeers { get; private set; } = [];
    public void Apply(EditPeerFoldersStartedSagaEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        TotalCount = aggregateEvent.TotalCount;
    }

    public void Apply(EditPeerFoldersCompletedSagaEvent aggregateEvent)
    {

    }

    public void Apply(EditPeerFoldersPtsIncrementedSagaEvent aggregateEvent)
    {
        if (Pts < aggregateEvent.Pts)
        {
            Pts = aggregateEvent.Pts;
        }

        PtsCount++;
    }

    public void Apply(EditPeerFoldersPeerFolderUpdatedSagaEvent aggregateEvent)
    {
        FolderPeers.Add(aggregateEvent.Peer);
    }
}


[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditPeerFoldersSagaId>))]
public class EditPeerFoldersSagaId(string value) : SingleValueObject<string>(value), ISagaId
{

}

public class EditPeerFoldersSagaLocator : DefaultSagaLocator<EditPeerFoldersSaga, EditPeerFoldersSagaId>
{
    protected override EditPeerFoldersSagaId CreateSagaId(string requestId)
    {
        return new EditPeerFoldersSagaId(requestId);
    }
}