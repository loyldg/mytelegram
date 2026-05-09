using EventFlow.Aggregates.ExecutionResults;
using MyTelegram.Messenger.Converters.ConverterServices;
using MyTelegram.Schema.Extensions;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class MessageDomainEventHandler(ICommandBus commandBus,
    IMessageConverterService messageConverterService,
    IQueuedCommandExecutor<MessageTokenAggregate, MessageTokenId, IExecutionResult> queuedCommandExecutor,
    ITokenizer tokenizer) :
    ISubscribeSynchronousTo<MessageAggregate, MessageId, OutboxMessageCreatedEvent>,
    ISubscribeSynchronousTo<MessageAggregate, MessageId, InboxMessageCreatedEvent>,
    ISubscribeSynchronousTo<MessageAggregate, MessageId, OutboxMessageEditedEventV2>,
    ISubscribeSynchronousTo<MessageAggregate, MessageId, InboxMessageEditedEventV2>,
    ISubscribeSynchronousTo<MessageAggregate, MessageId, MessageDeleted4Event>
{
    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, OutboxMessageCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        UpdateMessageTokens(domainEvent.AggregateEvent.OutboxMessageItem);

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        UpdateMessageTokens(domainEvent.AggregateEvent.InboxMessageItem);

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, OutboxMessageEditedEventV2> domainEvent, CancellationToken cancellationToken)
    {
        UpdateMessageTokens(domainEvent.AggregateEvent.NewMessageItem);

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageEditedEventV2> domainEvent, CancellationToken cancellationToken)
    {
        UpdateMessageTokens(domainEvent.AggregateEvent.NewMessageItem);

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageDeleted4Event> domainEvent, CancellationToken cancellationToken)
    {
        var command = new DeleteMessageTokenCommand(MessageTokenId.Create(domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId));
        return commandBus.PublishAsync(command, cancellationToken);
    }

    private void UpdateMessageTokens(MessageItem item)
    {
        var message = item.Message;
        if (string.IsNullOrEmpty(message) && item.EncryptedData != null)
        {
            message = messageConverterService.DecryptMessage(item.OwnerPeer.PeerId, item.MessageId,
                item.EncryptedData);
        }

        if (!string.IsNullOrEmpty(message) && message.Length > 3)
        {
            var tokens = tokenizer.BuildSearchTokens(message);

            if (tokens.Count > 0)
            {
                var command = new CreateMessageTokenCommand(MessageTokenId.Create(item.OwnerPeer.PeerId, item.MessageId),
                    item.OwnerPeer.PeerId,
                    item.MessageId,
                    item.ToPeer.PeerType,
                    item.ToPeer.PeerId,
                    item.SenderUserId,
                    item.SavedPeerId,
                    item.TopMsgId,
                    item.InputReplyTo.ToReplyToMsgId(),
                    item.MessageType,
                    item.Pinned,
                    item.Post,
                    item.Date,
                    item.PublicPosts,
                    item.Hashtags,
                    tokens);

                queuedCommandExecutor.Enqueue(command);
            }
        }
    }
}