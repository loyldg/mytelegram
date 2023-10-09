using EventFlow.Sagas;

namespace MyTelegram.Messenger.NativeAot;

public class MySagaAggregateStore : SagaStore
{
    private readonly IAggregateStore _aggregateStore;
    private readonly ICommandBus _commandBus;

    public MySagaAggregateStore(IAggregateStore aggregateStore,
        ICommandBus commandBus)
    {
        _aggregateStore = aggregateStore;
        _commandBus = commandBus;
    }

    public override async Task<ISaga> UpdateAsync(ISagaId sagaId,
        Type sagaType,
        ISourceId sourceId,
        Func<ISaga, CancellationToken, Task> updateSaga,
        CancellationToken cancellationToken)
    {
        ISaga saga = null;
        await UpdateInternalAsync(sagaId,
            sagaType,
            sourceId,
            async (s,
                c) =>
            {
                await updateSaga(s, c);
                saga = s;
            }, cancellationToken);
        if (saga == null)
        {
            return saga;
        }

        await saga.PublishAsync(_commandBus, cancellationToken);
        return saga;
    }

    private async Task<IReadOnlyCollection<IDomainEvent>> UpdateInternalAsync(ISagaId sagaId,
        Type sagaType,
        ISourceId sourceId,
        Func<ISaga, CancellationToken, Task> updateSaga,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<IDomainEvent> domainEvents;
        //var _aggregateStore = _serviceProvider.GetRequiredService<IAggregateStore>();
        switch (sagaId)
        {
           
            case DeleteMessageSaga2Id deleteMessageSaga2Id:
                domainEvents = await _aggregateStore
                    .UpdateAsync<DeleteMessageSaga2, DeleteMessageSaga2Id>(deleteMessageSaga2Id,
                        sourceId,
                        updateSaga,
                        cancellationToken);
                break;
            case AddChatUserSagaId addChatUserSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<AddChatUserSaga, AddChatUserSagaId>(addChatUserSagaId, sourceId, updateSaga, cancellationToken);
                break;
           
            case ClearHistorySagaId clearHistorySagaId:
                domainEvents = await _aggregateStore.UpdateAsync<ClearHistorySaga, ClearHistorySagaId>(clearHistorySagaId, sourceId, updateSaga, cancellationToken);

                break;
            case CreateChannelSagaId createChannelSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<CreateChannelSaga, CreateChannelSagaId>(createChannelSagaId, sourceId, updateSaga, cancellationToken);

                break;
            case CreateChatSagaId createChatSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<CreateChatSaga, CreateChatSagaId>(createChatSagaId, sourceId, updateSaga, cancellationToken);

                break;
           
            case DeleteChatUserSagaId deleteChatUserSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<DeleteChatUserSaga, DeleteChatUserSagaId>(deleteChatUserSagaId, sourceId, updateSaga, cancellationToken);

                break;
            case DeleteMessageSagaId deleteMessageSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<DeleteMessageSaga, DeleteMessageSagaId>(deleteMessageSagaId, sourceId, updateSaga, cancellationToken);

                break;
            case DeleteParticipantHistorySagaId deleteParticipantHistorySagaId:
                domainEvents = await _aggregateStore.UpdateAsync<DeleteParticipantHistorySaga, DeleteParticipantHistorySagaId>(deleteParticipantHistorySagaId, sourceId, updateSaga, cancellationToken);

                break;
            
            case EditChannelPhotoSagaId editChannelPhotoSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<EditChannelPhotoSaga, EditChannelPhotoSagaId>(editChannelPhotoSagaId, sourceId, updateSaga, cancellationToken);

                break;
            case EditChannelTitleSagaId editChannelTitleSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<EditChannelTitleSaga, EditChannelTitleSagaId>(editChannelTitleSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case EditChatPhotoSagaId editChatPhotoSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<EditChatPhotoSaga, EditChatPhotoSagaId>(editChatPhotoSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case EditChatTitleSagaId editChatTitleSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<EditChatTitleSaga, EditChatTitleSagaId>(editChatTitleSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case EditMessageSagaId editMessageSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<EditMessageSaga, EditMessageSagaId>(editMessageSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case ForwardMessageSagaId forwardMessageSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<ForwardMessageSaga, ForwardMessageSagaId>(forwardMessageSagaId, sourceId, updateSaga, cancellationToken);
                break;
            
            case InviteToChannelSagaId inviteToChannelSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<InviteToChannelSaga, InviteToChannelSagaId>(inviteToChannelSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case JoinChannelSagaId joinChannelSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<JoinChannelSaga, JoinChannelSagaId>(joinChannelSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case MessageSagaId messageSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<MessageSaga, MessageSagaId>(messageSagaId, sourceId, updateSaga, cancellationToken);
                break;
            
            case ReadChannelHistorySagaId readChannelHistorySagaId:
                domainEvents = await _aggregateStore.UpdateAsync<ReadChannelHistorySaga, ReadChannelHistorySagaId>(readChannelHistorySagaId, sourceId, updateSaga, cancellationToken);
                break;
            case ReadHistorySagaId readHistorySagaId:
                domainEvents = await _aggregateStore.UpdateAsync<ReadHistorySaga, ReadHistorySagaId>(readHistorySagaId, sourceId, updateSaga, cancellationToken);
                break;
            
            case SignInSagaId signInSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<SignInSaga, SignInSagaId>(signInSagaId, sourceId, updateSaga, cancellationToken);
                break;
           
            case UpdatePinnedMessageSagaId updatePinnedMessageSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>(updatePinnedMessageSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case UpdateUserNameSagaId updateUserNameSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<UpdateUserNameSaga, UpdateUserNameSagaId>(updateUserNameSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case UserSignUpSagaId userSignUpSagaId:
                domainEvents = await _aggregateStore.UpdateAsync<UserSignUpSaga, UserSignUpSagaId>(userSignUpSagaId, sourceId, updateSaga, cancellationToken);
                break;
           
            case VoteSagaId voteSagaId:
                domainEvents = await _aggregateStore
                    .UpdateAsync<VoteSaga, VoteSagaId>(voteSagaId, sourceId, updateSaga, cancellationToken)
             ;
                break;
            
            default:
                throw new ArgumentOutOfRangeException($"Should add {sagaType.Name} to {this.GetType().FullName}.UpdateAsync");
        }

        return domainEvents;
    }
}