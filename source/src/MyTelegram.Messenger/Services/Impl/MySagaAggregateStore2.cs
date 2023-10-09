//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using EventFlow.Aggregates;
//using EventFlow.Core;
//using EventFlow.Sagas;
//using MyTelegram.Domain.Sagas;
//using MyTelegram.Domain.Sagas.Identities;

//namespace MyTelegram.MessengerServer.Services.Impl;
//public class MySagaAggregateStore2 : SagaStore
//{
//    private readonly IAggregateStore _aggregateStore;
//    private readonly ICommandBus _commandBus;

//    public MySagaAggregateStore2(IAggregateStore aggregateStore,
//        ICommandBus commandBus)
//    {
//        _aggregateStore = aggregateStore;
//        _commandBus = commandBus;
//    }

//    public override async Task<ISaga> UpdateAsync(ISagaId sagaId,
//        Type sagaType,
//        ISourceId sourceId,
//        Func<ISaga, CancellationToken, Task> updateSaga,
//        CancellationToken cancellationToken)
//    {
//        ISaga saga = null;
//        await UpdateInternalAsync(sagaId,
//            sagaType,
//            sourceId,
//            async (s,
//                c) =>
//            {
//                await updateSaga(s, c);
//                saga = s;
//            },
//            cancellationToken);
//        if (saga == null)
//        {
//            return saga;
//        }

//        await saga.PublishAsync(_commandBus, cancellationToken);
//        return saga;
//    }

//    private async Task<IReadOnlyCollection<IDomainEvent>> UpdateInternalAsync(ISagaId sagaId,
//        Type sagaType,
//        ISourceId sourceId,
//        Func<ISaga, CancellationToken, Task> updateSaga,
//        CancellationToken cancellationToken)
//    {
//        IReadOnlyCollection<IDomainEvent> domainEvents = new List<IDomainEvent>();
//        //var _aggregateStore = _serviceProvider.GetRequiredService<IAggregateStore>();
//        switch (sagaId)
//        {
//            case AddChatUserSagaId addChatUserSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<AddChatUserSaga, AddChatUserSagaId>(addChatUserSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case ClearHistorySagaId clearHistorySagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<ClearHistorySaga, ClearHistorySagaId>(clearHistorySagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);

//                break;
//            case CreateChannelSagaId createChannelSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<CreateChannelSaga, CreateChannelSagaId>(createChannelSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);

//                break;
//            case CreateChatSagaId createChatSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<CreateChatSaga, CreateChatSagaId>(createChatSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);

//                break;
//            case DeleteChatUserSagaId deleteChatUserSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<DeleteChatUserSaga, DeleteChatUserSagaId>(deleteChatUserSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);

//                break;
//            case DeleteMessageSagaId deleteMessageSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<DeleteMessageSaga, DeleteMessageSagaId>(deleteMessageSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);

//                break;
//            case EditChannelPhotoSagaId editChannelPhotoSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<EditChannelPhotoSaga, EditChannelPhotoSagaId>(editChannelPhotoSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);

//                break;
//            case EditChannelTitleSagaId editChannelTitleSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<EditChannelTitleSaga, EditChannelTitleSagaId>(editChannelTitleSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case EditChatPhotoSagaId editChatPhotoSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<EditChatPhotoSaga, EditChatPhotoSagaId>(editChatPhotoSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case EditChatTitleSagaId editChatTitleSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<EditChatTitleSaga, EditChatTitleSagaId>(editChatTitleSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case EditMessageSagaId editMessageSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<EditMessageSaga, EditMessageSagaId>(editMessageSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case ForwardMessageSagaId forwardMessageSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<ForwardMessageSaga, ForwardMessageSagaId>(forwardMessageSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case ImportContactsSagaId importContactsSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<ImportContactsSaga, ImportContactsSagaId>(importContactsSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case InviteToChannelSagaId inviteToChannelSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<InviteToChannelSaga, InviteToChannelSagaId>(inviteToChannelSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case JoinChannelSagaId joinChannelSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<JoinChannelSaga, JoinChannelSagaId>(joinChannelSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case MessageSagaId messageSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<MessageSaga, MessageSagaId>(messageSagaId, sourceId, updateSaga, cancellationToken)
//             ;
//                break;
//            case PhoneCallSagaId phoneCallSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<PhoneCallSaga, PhoneCallSagaId>(phoneCallSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case ReadChannelHistorySagaId readChannelHistorySagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<ReadChannelHistorySaga, ReadChannelHistorySagaId>(readChannelHistorySagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case ReadHistorySagaId readHistorySagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<ReadHistorySaga, ReadHistorySagaId>(readHistorySagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case RecoveryPasswordSagaId recoveryPasswordSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<RecoveryPasswordSaga, RecoveryPasswordSagaId>(recoveryPasswordSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case SendEncryptedMessageSagaId sendEncryptedMessageSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<SendEncryptedMessageSaga, SendEncryptedMessageSagaId>(sendEncryptedMessageSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case SignInSagaId signInSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<SignInSaga, SignInSagaId>(signInSagaId, sourceId, updateSaga, cancellationToken)
//             ;
//                break;
//            case UpdatePinnedMessageSagaId updatePinnedMessageSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>(updatePinnedMessageSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case UpdateUserNameSagaId updateUserNameSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<UpdateUserNameSaga, UpdateUserNameSagaId>(updateUserNameSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            case UserSignUpSagaId userSignUpSagaId:
//                domainEvents = await _aggregateStore
//                    .UpdateAsync<UserSignUpSaga, UserSignUpSagaId>(userSignUpSagaId,
//                        sourceId,
//                        updateSaga,
//                        cancellationToken);
//                break;
//            default:
//                throw new ArgumentOutOfRangeException(
//                    $"Should add {sagaType.Name} to MySagaAggregateStore2.UpdateAsync");
//        }

//        return domainEvents;
//    }
//}