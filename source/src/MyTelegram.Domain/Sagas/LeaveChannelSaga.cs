//namespace MyTelegram.Domain.Sagas;

//// [Newtonsoft.Json.JsonConverter(typeof(SingleValueObjectConverter))]
//// [System.Text.Json.Serialization.JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<LeaveChannelSagaId>))]
//// public class LeaveChannelSagaId : SingleValueObject<string>, ISagaId
//// {
////     public LeaveChannelSagaId(string value) : base(value)
////     {
////     }
//// }
////
//// public class LeaveChannelSagaLocator : ISagaLocator
//// {
////     public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent,
////         CancellationToken cancellationToken)
////     {
////         var id = domainEvent.GetAggregateEvent() as IHasCorrelationId;
////         if (id == null)
////         {
////             throw new NotSupportedException(
////                 $"Domain event:{domainEvent.GetAggregateEvent().GetType().FullName} should impl IHasCorrelationId ");
////         }
////
////         return Task.FromResult<ISagaId>(new LeaveChannelSagaId($"leavechannelsaga-{id.CorrelationId}"));
////     }
//// }
////
//// public class LeaveChannelSaga : AggregateSaga<LeaveChannelSaga, LeaveChannelSagaId, LeaveChannelSagaLocator>
//// {
//// }


