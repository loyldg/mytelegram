//using MyTelegram.Domain.Aggregates.PushUpdates;
//using MyTelegram.Domain.Events.PushUpdates;

//namespace MyTelegram.MessengerServer.DomainEventHandlers;

////public class
////    PushUpdatesCreatedEventHandler : ISubscribeSynchronousTo<PushUpdatesAggregate, PushUpdatesId,
////        PushUpdatesCreatedEvent>
////{
////    public Task HandleAsync(IDomainEvent<PushUpdatesAggregate, PushUpdatesId, PushUpdatesCreatedEvent> domainEvent,
////        CancellationToken cancellationToken)
////    {
////        //Console.WriteLine($"GlobalSeqNo:{domainEvent.AggregateEvent.SeqNo}");
////        //if (domainEvent.AggregateEvent.PeerType != PeerType.Channel)
////        //{
////        //    return _sequenceService.SetPtsForPeerAsync(domainEvent.AggregateEvent.PeerId,
////        //        domainEvent.AggregateEvent.PeerType,
////        //        domainEvent.AggregateEvent.Pts);
////        //}
////        return Task.CompletedTask;
////        //throw new NotImplementedException();
////    }
////}