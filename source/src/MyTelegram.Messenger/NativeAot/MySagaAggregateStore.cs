using EventFlow.Sagas;
using MyTelegram.Domain.Sagas;

namespace MyTelegram.Messenger.NativeAot;

public class MySagaAggregateStore(
    IAggregateStore aggregateStore,
    ICommandBus commandBus)
    : SagaStore, ITransientDependency
{
    public override async Task<ISaga> UpdateAsync(ISagaId sagaId,
        Type sagaType,
        ISourceId sourceId,
        Func<ISaga, CancellationToken, Task> updateSaga,
        CancellationToken cancellationToken)
    {
        ISaga? saga = null;
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

        await saga.PublishAsync(commandBus, cancellationToken);
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
            case PinForwardedChannelMessageSagaId pinForwardedChannelMessageSagaId:
                domainEvents = await aggregateStore.UpdateAsync<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId>(pinForwardedChannelMessageSagaId, sourceId, updateSaga, cancellationToken);

                break;
            case SetDiscussionGroupSagaId setDiscussionGroupSagaId:
                domainEvents = await aggregateStore.UpdateAsync<SetDiscussionGroupSaga, SetDiscussionGroupSagaId>(setDiscussionGroupSagaId, sourceId, updateSaga, cancellationToken);

                break;
            case UnpinAllMessagesSagaId unPinAllMessagesSagaId:
                domainEvents = await aggregateStore.UpdateAsync<UnpinAllMessagesSaga, UnpinAllMessagesSagaId>(unPinAllMessagesSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case UpdateMessagePinnedSagaId updateMessagePinnedSagaId:
                domainEvents = await aggregateStore.UpdateAsync<UpdateMessagePinnedSaga, UpdateMessagePinnedSagaId>(updateMessagePinnedSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case UpdateMessageReplySagaId updateMessageReplySagaId:
                domainEvents = await aggregateStore.UpdateAsync<UpdateMessageReplySaga, UpdateMessageReplySagaId>(updateMessageReplySagaId, sourceId, updateSaga, cancellationToken);
                break;
            case UploadProfilePhotoSagaId uploadProfilePhotoSagaId:
                domainEvents = await aggregateStore.UpdateAsync<UploadProfilePhotoSaga, UploadProfilePhotoSagaId>(uploadProfilePhotoSagaId, sourceId, updateSaga, cancellationToken);
                break;

            case DeleteChannelMessagesSagaId deleteChannelMessagesSagaId:
                domainEvents = await aggregateStore.UpdateAsync<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId>(deleteChannelMessagesSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case DeleteMessagesSaga4Id deleteMessagesSaga4Id:
                domainEvents = await aggregateStore.UpdateAsync<DeleteMessagesSaga4, DeleteMessagesSaga4Id>(deleteMessagesSaga4Id, sourceId, updateSaga, cancellationToken);

                break;
            case DeleteReplyMessagesSagaId deleteReplyMessagesSagaId:
                domainEvents = await aggregateStore.UpdateAsync<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId>(deleteReplyMessagesSagaId, sourceId, updateSaga, cancellationToken);

                break;
            case EditPeerFoldersSagaId editPeerFoldersSagaId:
                domainEvents = await aggregateStore.UpdateAsync<EditPeerFoldersSaga, EditPeerFoldersSagaId>(editPeerFoldersSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case EditExportedChatInviteSagaId editExportedChatInviteSagaId:
                domainEvents = await aggregateStore.UpdateAsync<EditExportedChatInviteSaga, EditExportedChatInviteSagaId>(editExportedChatInviteSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case ImportChatInviteSagaId importChatInviteSagaId:
                domainEvents = await aggregateStore.UpdateAsync<ImportChatInviteSaga, ImportChatInviteSagaId>(importChatInviteSagaId, sourceId, updateSaga, cancellationToken);
                break;

            case ApproveJoinChannelSagaId approveJoinChannelSagaId:
                domainEvents = await aggregateStore.UpdateAsync<ApproveJoinChannelSaga, ApproveJoinChannelSagaId>(approveJoinChannelSagaId, sourceId, updateSaga, cancellationToken);

                break;
            case ClearHistorySagaId clearHistorySagaId:
                domainEvents = await aggregateStore.UpdateAsync<ClearHistorySaga, ClearHistorySagaId>(clearHistorySagaId, sourceId, updateSaga, cancellationToken);

                break;
            case CreateChannelSagaId createChannelSagaId:
                domainEvents = await aggregateStore.UpdateAsync<CreateChannelSaga, CreateChannelSagaId>(createChannelSagaId, sourceId, updateSaga, cancellationToken);

                break;
           
            case CreateUserSagaId createUserSagaId:
                domainEvents = await aggregateStore.UpdateAsync<CreateUserSaga, CreateUserSagaId>(createUserSagaId, sourceId, updateSaga, cancellationToken);
                break;
          
            case EditAdminSagaId editAdminSagaId:
                domainEvents = await aggregateStore.UpdateAsync<EditAdminSaga, EditAdminSagaId>(editAdminSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case EditBannedSagaId editBannedSagaId:
                domainEvents = await aggregateStore.UpdateAsync<EditBannedSaga, EditBannedSagaId>(editBannedSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case EditChannelPhotoSagaId editChannelPhotoSagaId:
                domainEvents = await aggregateStore.UpdateAsync<EditChannelPhotoSaga, EditChannelPhotoSagaId>(editChannelPhotoSagaId, sourceId, updateSaga, cancellationToken);

                break;
            case EditChannelTitleSagaId editChannelTitleSagaId:
                domainEvents = await aggregateStore.UpdateAsync<EditChannelTitleSaga, EditChannelTitleSagaId>(editChannelTitleSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case EditMessageSagaId editMessageSagaId:
                domainEvents = await aggregateStore.UpdateAsync<EditMessageSaga, EditMessageSagaId>(editMessageSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case ForwardMessageSagaId forwardMessageSagaId:
                domainEvents = await aggregateStore.UpdateAsync<ForwardMessageSaga, ForwardMessageSagaId>(forwardMessageSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case ImportContactsSagaId importContactsSagaId:
                domainEvents = await aggregateStore.UpdateAsync<ImportContactsSaga, ImportContactsSagaId>(importContactsSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case InviteToChannelSagaId inviteToChannelSagaId:
                domainEvents = await aggregateStore.UpdateAsync<InviteToChannelSaga, InviteToChannelSagaId>(inviteToChannelSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case JoinChannelSagaId joinChannelSagaId:
                domainEvents = await aggregateStore.UpdateAsync<JoinChannelSaga, JoinChannelSagaId>(joinChannelSagaId, sourceId, updateSaga, cancellationToken);
                break;
            //case MessageSagaId messageSagaId:
            //    domainEvents = await _aggregateStore.UpdateAsync<MessageSaga, MessageSagaId>(messageSagaId, sourceId, updateSaga, cancellationToken);
            //    break;

            case ReadChannelHistorySagaId readChannelHistorySagaId:
                domainEvents = await aggregateStore.UpdateAsync<ReadChannelHistorySaga, ReadChannelHistorySagaId>(readChannelHistorySagaId, sourceId, updateSaga, cancellationToken);
                break;
            case ReadHistorySagaId readHistorySagaId:
                domainEvents = await aggregateStore.UpdateAsync<ReadHistorySaga, ReadHistorySagaId>(readHistorySagaId, sourceId, updateSaga, cancellationToken);
                break;
            case ReadMentionsSagaId readMentionsSagaId:
                domainEvents = await aggregateStore.UpdateAsync<ReadMentionsSaga, ReadMentionsSagaId>(readMentionsSagaId, sourceId, updateSaga, cancellationToken);
                break;

            case SignInSagaId signInSagaId:
                domainEvents = await aggregateStore.UpdateAsync<SignInSaga, SignInSagaId>(signInSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case UpdateContactProfilePhotoSagaId updateContactProfilePhotoSagaId:
                domainEvents = await aggregateStore.UpdateAsync<UpdateContactProfilePhotoSaga, UpdateContactProfilePhotoSagaId>(updateContactProfilePhotoSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case UpdatePinnedMessageSagaId updatePinnedMessageSagaId:
                domainEvents = await aggregateStore.UpdateAsync<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>(updatePinnedMessageSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case UpdateUserNameSagaId updateUserNameSagaId:
                domainEvents = await aggregateStore.UpdateAsync<UpdateUserNameSaga, UpdateUserNameSagaId>(updateUserNameSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case UserSignUpSagaId userSignUpSagaId:
                domainEvents = await aggregateStore.UpdateAsync<UserSignUpSaga, UserSignUpSagaId>(userSignUpSagaId, sourceId, updateSaga, cancellationToken);
                break;
           
            case VoteSagaId voteSagaId:
                domainEvents = await aggregateStore.UpdateAsync<VoteSaga, VoteSagaId>(voteSagaId, sourceId, updateSaga, cancellationToken);
                break;
            case LeaveChannelSagaId leaveChannelSagaId:
                domainEvents = await aggregateStore.UpdateAsync<LeaveChannelSaga, LeaveChannelSagaId>(leaveChannelSagaId, sourceId, updateSaga, cancellationToken);
                break;

            case SendMessageSagaId sendMessageSagaId:
                domainEvents = await aggregateStore
                        .UpdateAsync<SendMessageSaga, SendMessageSagaId>(sendMessageSagaId, sourceId, updateSaga, cancellationToken)
                    ;
                break;
            default:
                throw new ArgumentOutOfRangeException($"Should add {sagaType.Name} to {this.GetType().FullName}.UpdateAsync");
        }

        return domainEvents;
    }
}