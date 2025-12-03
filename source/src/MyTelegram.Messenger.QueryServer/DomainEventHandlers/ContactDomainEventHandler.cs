using MyTelegram.Messenger.Services.Interfaces;
using TPeerSettings = MyTelegram.Schema.TPeerSettings;
using TPhoto = MyTelegram.Schema.Photos.TPhoto;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ContactDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IUserConverterService userConverterService,
    IPhotoAppService photoAppService,
    ILayeredService<IPhotoConverter> photoLayeredService
    )
    : DomainEventHandlerBase(objectMessageSender,
            commandBus,
            idGenerator,
            ackCacheService),
        ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactAddedEvent>,
        ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactDeletedEvent>,
        ISubscribeSynchronousTo<ImportContactsSaga, ImportContactsSagaId, ImportContactsCompletedSagaEvent>,
        ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactProfilePhotoChangedEvent>
{
    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactAddedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var user = await userConverterService.GetUserAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.TargetUserId,
            true,
            false,
            domainEvent.AggregateEvent.RequestInfo.Layer
        );
        user.Contact = true;
        user.FirstName = domainEvent.AggregateEvent.FirstName;
        user.LastName = domainEvent.AggregateEvent.LastName;

        var r = new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0,
            Updates = new TVector<IUpdate>(new TUpdatePeerSettings
            {
                Peer = new TPeerUser { UserId = domainEvent.AggregateEvent.TargetUserId },
                Settings = new TPeerSettings { NeedContactsException = false }
            }),
            Users = new TVector<IUser>(user)
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var user = await userConverterService.GetUserAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.TargetUserId,
            true, false,
            domainEvent.AggregateEvent.RequestInfo.Layer
        );

        var r = new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0,
            Updates = new TVector<IUpdate>(new TUpdatePeerSettings
            {
                Peer = new TPeerUser { UserId = domainEvent.AggregateEvent.TargetUserId },
                Settings = new TPeerSettings()
            }),
            Users = new TVector<IUser>(user)
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(
        IDomainEvent<ContactAggregate, ContactId, ContactProfilePhotoChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var user = await userConverterService.GetUserAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.TargetUserId,
            false,
            false,
            domainEvent.AggregateEvent.RequestInfo.Layer
        );
        var newPhotoId = domainEvent.AggregateEvent.PhotoId;
        if (newPhotoId == 0)
        {
            switch (user.Photo)
            {
                case TUserProfilePhoto userProfilePhoto:
                    newPhotoId = userProfilePhoto.PhotoId;
                    break;
            }
        }

        IPhotoReadModel? photoReadModel = null;
        if (newPhotoId != 0)
        {
            await photoAppService.GetAsync(newPhotoId);
        }
        var photo = photoLayeredService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToPhoto(photoReadModel);
        var r = new TPhoto
        {
            Users = new TVector<IUser>(user),
            Photo = photo
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(
        IDomainEvent<ImportContactsSaga, ImportContactsSagaId, ImportContactsCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var importedContacts = domainEvent.AggregateEvent.PhoneContacts
            .Where(p => p.UserId > 0)
            .Select(p => new TImportedContact { ClientId = p.ClientId, UserId = p.UserId }).ToList();
        var userIds = importedContacts.Select(p => p.UserId).ToList();
        var users = await userConverterService.GetUserListAsync(domainEvent.AggregateEvent.RequestInfo,
            userIds, true, false, domainEvent.AggregateEvent.RequestInfo.Layer
        );

        foreach (var layeredUser in users)
        {
            layeredUser.Contact = true;
        }

        var r = new TImportedContacts
        {
            Imported = new TVector<IImportedContact>(importedContacts),
            PopularInvites = new TVector<IPopularContact>(),
            RetryContacts = new TVector<long>(),
            Users = new TVector<IUser>(users)
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);

        var updates = new List<IUpdate>();
        foreach (var userId in userIds)
        {
            var updatePeerSettings = new TUpdatePeerSettings
            {
                Peer = new TPeerUser
                {
                    UserId = userId
                },
                Settings = new TPeerSettings()
            };
            updates.Add(updatePeerSettings);
        }

        await PushMessageToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), new TUpdates
        {
            Updates = new TVector<IUpdate>(updates),
            Users = new TVector<IUser>(users),
            Chats = [],
            Date = DateTime.Now.ToTimestamp()
        });
    }
}