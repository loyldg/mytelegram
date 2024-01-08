using MyTelegram.Domain.Aggregates.Updates;
using MyTelegram.Domain.Events.Updates;
using MyTelegram.Schema;

namespace MyTelegram.ReadModel.Impl;

//public class PushUpdatesReadModel : IPushUpdatesReadModel,
//    IAmReadModelFor<PushUpdatesAggregate, PushUpdatesId, PushUpdatesCreatedEvent>
//{
//    public virtual long? Version { get; set; }

//    public Task ApplyAsync(IReadModelContext context,
//        IDomainEvent<PushUpdatesAggregate, PushUpdatesId, PushUpdatesCreatedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        Id = domainEvent.AggregateIdentity.Value;
//        PeerId = domainEvent.AggregateEvent.ToPeer.PeerId;
//        OnlySendToThisAuthKeyId = domainEvent.AggregateEvent.OnlySendToThisAuthKeyId;
//        ExcludeAuthKeyId = domainEvent.AggregateEvent.ExcludeAuthKeyId;
//        Data = domainEvent.AggregateEvent.Data;
//        Pts = domainEvent.AggregateEvent.Pts;
//        PtsType = domainEvent.AggregateEvent.PtsType;
//        SeqNo = domainEvent.AggregateEvent.SeqNo;
//        PeerIdList= domainEvent.AggregateEvent.PeerIdList;
//        MessageId= domainEvent.AggregateEvent.MessageId;

//        return Task.CompletedTask;
//    }

//    public virtual string Id { get; private set; } = null!;
//    public long OwnerPeerId { get; }
//    public long? ExcludeAuthKeyId { get; }
//    public long? OnlySendToThisAuthKeyId { get; }
//    public PtsType PtsType { get; }
//    public int Pts { get; }
//    public int? MessageId { get; }
//    public int Date { get; }
//    public long SeqNo { get; }
//    public List<long>? Users { get; }
//    public List<long>? Chats { get; }
//}

public class UpdatesReadModel : IUpdatesReadModel,
    IAmReadModelFor<UpdatesAggregate, UpdatesId, UpdatesCreatedEvent>
{
    public long OwnerPeerId { get; private set; }

    public long ChannelId { get; private set; }

    //public long? ChannelId { get; private set; }
    public long? ExcludeAuthKeyId { get; private set; }
    public long? ExcludeUserId { get; private set; }
    public long? OnlySendToUserId { get; private set; }
    public long? OnlySendToThisAuthKeyId { get; private set; }
    public UpdatesType UpdatesType { get; set; }
    //public PtsType PtsType { get; private set; }
    public int? MessageId { get; private set; }
    public int Pts { get; private set; }
    public int Date { get; private set; }
    public long GlobalSeqNo { get; private set; }
    public IList<IUpdate>? Updates { get; private set; }
    public List<long>? Users { get; private set; }
    public List<long>? Chats { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual long? Version { get; set; }



    public Task ApplyAsync(IReadModelContext context, IDomainEvent<UpdatesAggregate, UpdatesId, UpdatesCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;

        OwnerPeerId = domainEvent.AggregateEvent.OwnerPeerId;
        if (OwnerPeerId > MyTelegramServerDomainConsts.ChannelInitId)
        {
            ChannelId = OwnerPeerId;
        }

        ExcludeAuthKeyId = domainEvent.AggregateEvent.ExcludeAuthKeyId;
        ExcludeUserId = domainEvent.AggregateEvent.ExcludeUserId;
        OnlySendToUserId = domainEvent.AggregateEvent.OnlySendToUserId;
        OnlySendToThisAuthKeyId = domainEvent.AggregateEvent.OnlySendToThisAuthKeyId;
        //PtsType = domainEvent.AggregateEvent.PtsType;
        UpdatesType = domainEvent.AggregateEvent.UpdatesType;
        Pts = domainEvent.AggregateEvent.Pts;
        MessageId = domainEvent.AggregateEvent.MessageId;
        Date = domainEvent.AggregateEvent.Date;
        GlobalSeqNo = domainEvent.AggregateEvent.GlobalSeqNo;
        Updates = domainEvent.AggregateEvent.Updates;
        Users = domainEvent.AggregateEvent.Users;
        Chats = domainEvent.AggregateEvent.Chats;
        //ChannelId=domainEvent.AggregateEvent.ChannelId;

        return Task.CompletedTask;
    }
}