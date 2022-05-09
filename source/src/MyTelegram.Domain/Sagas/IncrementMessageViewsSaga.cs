//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using EventFlow.Aggregates;
//using EventFlow.Sagas;
//using EventFlow.Sagas.AggregateSagas;
//using EventFlow.ValueObjects;
//using MyTelegramServer.Domain.Aggregates.MessageBox;
//using MyTelegramServer.Domain.Commands.MessageBox;
//using MyTelegramServer.Domain.Events;
//using MyTelegramServer.Domain.Events.MessageBox;

//namespace MyTelegramServer.Domain.Sagas
//{
//    [Newtonsoft.Json.JsonConverter(typeof(SingleValueObjectConverter))]
//    [System.Text.Json.Serialization.JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<IncrementViewsSagaId>))]
//    public class IncrementViewsSagaId : SingleValueObject<string>, ISagaId
//    {
//        public IncrementViewsSagaId(string value) : base(value)
//        {
//        }
//    }

//    public class IncrementViewsSagaLocator : ISagaLocator
//    {
//        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent,
//            CancellationToken cancellationToken)
//        {
//            var id = domainEvent.GetAggregateEvent() as IHasCorrelationId;
//            if (id == null)
//            {
//                throw new NotSupportedException(
//                    $"Domain event:{domainEvent.GetAggregateEvent().GetType().FullName} should impl IHasCorrelationId ");
//            }
//            var messageSagaId = new IncrementViewsSagaId($"incrementviewssaga-{id.CorrelationId}");

//            return Task.FromResult<ISagaId>(messageSagaId);
//        }
//    }

//    public class IncrementViewsSagaState : AggregateState<IncrementViewsSaga, IncrementViewsSagaId,
//        IncrementViewsSagaState>,
//        IApply<IncrementAllViewsStartedEvent>,
//        IApply<IncrementViewsSuccessEvent>,
//        IApply<IncrementViewsCheckLogSuccess>
//    //IApply<IncrementAllViewsSuccessEvent>
//    {
//        public long ChannelId { get; private set; }
//        public int TotalCount { get; private set; }
//        //public int IncrementedCount { get; private set; }
//        public long ReqMsgId { get; private set; }
//        public bool Completed => TotalCount == _messageIdToViews.Count;
//        //public Guid CorrelationId { get; private set; }

//        private readonly Dictionary<int, int> _messageIdToViews = new();
//        public Dictionary<int, int> MessageIdToViews => _messageIdToViews;

//        public void Apply(IncrementAllViewsStartedEvent aggregateEvent)
//        {
//            TotalCount = aggregateEvent.TotalCount;
//            ReqMsgId = aggregateEvent.ReqMsgId;
//            ChannelId = aggregateEvent.ChannelId;
//            //CorrelationId = aggregateEvent.CorrelationId;
//        }

//        public void Apply(IncrementViewsSuccessEvent aggregateEvent)
//        {
//            //IncrementedCount++;
//            _messageIdToViews.TryAdd(aggregateEvent.MessageId, aggregateEvent.Views);
//        }

//        //public void Apply(IncrementAllViewsSuccessEvent aggregateEvent)
//        //{
//        //    //throw new NotImplementedException();
//        //}
//        public void Apply(IncrementViewsCheckLogSuccess aggregateEvent)
//        {
//            //throw new NotImplementedException();
//        }
//    }

//    public class IncrementViewsSaga : AggregateSaga<IncrementViewsSaga, IncrementViewsSagaId, IncrementViewsSagaLocator>,
//        ISagaIsStartedBy<MessageBoxAggregate, MessageBoxId, IncrementViewsStartedEvent>,
//        ISagaHandles<MessageBoxAggregate, MessageBoxId, ViewsIncrementedEvent>,
//        ISagaHandles<MessageViewLogAggregate, MessageViewLogId, CheckMessageViewLogSuccessEvent>,
//        IApply<IncrementViewsCompletedEvent>
//    {
//        private readonly IncrementViewsSagaState _state = new();
//        public IncrementViewsSaga(IncrementViewsSagaId id) : base(id)
//        {
//            Register(_state);
//        }
//        public Task HandleAsync(IDomainEvent<MessageBoxAggregate, MessageBoxId, IncrementViewsStartedEvent> domainEvent,
//            ISagaContext sagaContext,
//            CancellationToken cancellationToken)
//        {
//            //var count = domainEvent.AggregateEvent.MessageIdList.Count(p => p > 0);
//            Emit(new IncrementAllViewsStartedEvent(domainEvent.AggregateEvent.ReqMsgId, domainEvent.AggregateEvent.MessageIdList.Count, domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.CorrelationId));
//            foreach (var messageId in domainEvent.AggregateEvent.MessageIdList)
//            {
//                if (messageId > 0)
//                {
//                    //var command = new IncrementViewsCommand(
//                    //    MessageBoxId.Create(domainEvent.AggregateEvent.ChannelId,
//                    //        domainEvent.AggregateEvent.ChannelId,
//                    //        messageId),
//                    //    domainEvent.AggregateEvent.ReqMsgId,
//                    //    messageId,
//                    //    domainEvent.AggregateEvent.CorrelationId);
//                    //Publish(command);

//                    var command = new CheckMessageViewLogCommand(
//                        MessageViewLogId.Create(domainEvent.AggregateEvent.ChannelId,
//                            domainEvent.AggregateEvent.UserId,
//                            messageId),
//                        messageId, domainEvent.AggregateEvent.CorrelationId);
//                    Publish(command);
//                }
//                else
//                {
//                    Emit(new IncrementViewsSuccessEvent(messageId, 0));
//                }
//            }

//            return Task.CompletedTask;
//        }
//        public Task HandleAsync(IDomainEvent<MessageBoxAggregate, MessageBoxId, ViewsIncrementedEvent> domainEvent,
//            ISagaContext sagaContext,
//            CancellationToken cancellationToken)
//        {
//            Emit(new IncrementViewsSuccessEvent(domainEvent.AggregateEvent.MessageId, domainEvent.AggregateEvent.Views));
//            HandleIncrementViewsCompleted();
//            return Task.CompletedTask;
//        }

//        private void HandleIncrementViewsCompleted()
//        {
//            if (_state.Completed)
//            {
//                Emit(new IncrementViewsCompletedEvent(_state.ReqMsgId, _state.MessageIdToViews));
//            }
//        }

//        public void Apply(IncrementViewsCompletedEvent aggregateEvent)
//        {
//            Complete();
//        }

//        public Task HandleAsync(IDomainEvent<MessageViewLogAggregate, MessageViewLogId, CheckMessageViewLogSuccessEvent> domainEvent,
//            ISagaContext sagaContext,
//            CancellationToken cancellationToken)
//        {
//            Emit(new IncrementViewsCheckLogSuccess(domainEvent.AggregateEvent.MessageId, domainEvent.AggregateEvent.AlreadyIncremented));

//            var command = new IncrementViewsCommand(
//                MessageBoxId.Create(
//                    _state.ChannelId,
//                    //PeerType.Channel,
//                    //_state.ChannelId,
//                    domainEvent.AggregateEvent.MessageId),
//                _state.ReqMsgId,
//                domainEvent.AggregateEvent.MessageId,
//                domainEvent.AggregateEvent.AlreadyIncremented,
//                domainEvent.AggregateEvent.CorrelationId);
//            Publish(command);

//            return Task.CompletedTask;
//        }
//    }

//    public class IncrementViewsCheckLogSuccess : AggregateEvent<IncrementViewsSaga, IncrementViewsSagaId>
//    {
//        public IncrementViewsCheckLogSuccess(int messageId,
//            bool alreadyIncremented)
//        {
//            MessageId = messageId;
//            AlreadyIncremented = alreadyIncremented;
//        }

//        public int MessageId { get; }
//        public bool AlreadyIncremented { get; }
//    }

//    public class
//        IncrementViewsCompletedEvent : RequestAggregateEvent<IncrementViewsSaga, IncrementViewsSagaId>
//    {
//        public IncrementViewsCompletedEvent(long reqMsgId, Dictionary<int, int> messageIdToViews) : base(reqMsgId)
//        {
//            MessageIdToViews = messageIdToViews;
//        }

//        public Dictionary<int, int> MessageIdToViews { get; }
//    }

//    public class
//        IncrementViewsSuccessEvent : AggregateEvent<IncrementViewsSaga, IncrementViewsSagaId>
//    {
//        public IncrementViewsSuccessEvent(int messageId,
//            int views)
//        {
//            MessageId = messageId;
//            Views = views;
//        }

//        public int MessageId { get; }
//        public int Views { get; }
//    }

//    public class
//        IncrementAllViewsStartedEvent : RequestAggregateEvent<IncrementViewsSaga, IncrementViewsSagaId>, IHasCorrelationId
//    {
//        public IncrementAllViewsStartedEvent(long reqMsgId, int totalCount, long channelId,
//            Guid correlationId) : base(reqMsgId)
//        {
//            TotalCount = totalCount;
//            ChannelId = channelId;
//            CorrelationId = correlationId;
//        }

//        public int TotalCount { get; }
//        public long ChannelId { get; }
//        public Guid CorrelationId { get; }
//    }
//}


