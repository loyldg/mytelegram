namespace MyTelegram.ReadModel.Impl;

public class AppCodeReadModel :
    IAppCodeReadModel,
    IAmReadModelFor<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<AppCodeAggregate, AppCodeId, AppCodeCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        AppCodeId = domainEvent.AggregateIdentity.Value;
        PhoneNumber = domainEvent.AggregateEvent.PhoneNumber;
        Code = domainEvent.AggregateEvent.Code;
        Expire = domainEvent.AggregateEvent.Expire;
        PhoneCodeHash = domainEvent.AggregateEvent.PhoneCodeHash;
        CreationTime = domainEvent.AggregateEvent.CreationTime;

        return Task.CompletedTask;
    }

    public virtual string AppCodeId { get; private set; } = null!;
    public virtual string Code { get; private set; } = null!;
    public virtual long CreationTime { get; private set; }
    public virtual int Expire { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual string PhoneCodeHash { get; private set; } = null!;
    public virtual string PhoneNumber { get; private set; } = null!;
}

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
    public byte[] Updates { get; private set; }
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