using EventFlow.ReadStores;
using MyTelegram.EventFlow;
using MyTelegram.EventFlow.ReadStores;

namespace MyTelegram.Messenger.Services.Impl;

public class ReadModelWriteInterceptor : IReadModelWriteInterceptor, ITransientDependency
{
    public void OnUpdate<TReadModel>(IReadModelContext readModelContext, TReadModel readModel, IReadOnlyCollection<IDomainEvent> domainEvents) where TReadModel : IReadModel
    {
        if (readModel is IMyReadModel myReadModel)
        {
            if (readModelContext.IsNew)
            {
                myReadModel.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }
            else
            {
                if (readModelContext.IsMarkedForDeletion)
                {
                    myReadModel.IsDeleted = true;
                    myReadModel.DeletionTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                }
                else
                {
                    myReadModel.LastModificationTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                }
            }

            foreach (var domainEvent in domainEvents)
            {
                var aggregateEvent = domainEvent.GetAggregateEvent();
                if (aggregateEvent is IHasRequestInfo hasRequestInfo)
                {
                    if (readModelContext.IsNew)
                    {
                        myReadModel.CreatedBy = hasRequestInfo.RequestInfo.UserId;
                    }
                    else
                    {
                        if (readModelContext.IsMarkedForDeletion)
                        {
                            myReadModel.DeletedBy = hasRequestInfo.RequestInfo.UserId;
                        }
                        else
                        {
                            myReadModel.LastModifiedBy = hasRequestInfo.RequestInfo.UserId;
                        }
                    }
                }
            }
        }
    }
}
