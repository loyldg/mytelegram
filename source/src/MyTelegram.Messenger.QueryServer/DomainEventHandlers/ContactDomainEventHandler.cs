using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Schema.Contacts;
using MyTelegram.Services.TLObjectConverters;
using TPeerSettings = MyTelegram.Schema.TPeerSettings;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ContactDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactAddedEvent>,
    ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactDeletedEvent>,
    ISubscribeSynchronousTo<ImportContactsSaga, ImportContactsSagaId, ImportContactsCompletedEvent>,
    ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactProfilePhotoChangedEvent>
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly IPhotoAppService _photoAppService;
    private readonly ILayeredService<IPhotoConverter> _layeredPhotoService;
    private readonly IPrivacyAppService _privacyAppService;

    public ContactDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService, IQueryProcessor queryProcessor, ILayeredService<IUserConverter> layeredUserService, IPhotoAppService photoAppService, ILayeredService<IPhotoConverter> layeredPhotoService, IPrivacyAppService privacyAppService) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _queryProcessor = queryProcessor;
        _layeredUserService = layeredUserService;
        _photoAppService = photoAppService;
        _layeredPhotoService = layeredPhotoService;
        _privacyAppService = privacyAppService;
    }

    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactAddedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var targetUser =
            await _queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.TargetUserId), default);
        var photos = await _photoAppService.GetPhotosAsync(targetUser);
        var privacies = await _privacyAppService.GetPrivacyListAsync(domainEvent.AggregateEvent.TargetUserId);
        var tUser = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent.SelfUserId, targetUser!, photos, null, privacies);
        tUser.Contact = true;
        tUser.FirstName = domainEvent.AggregateEvent.FirstName;
        tUser.LastName = domainEvent.AggregateEvent.LastName;

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
            Users = new TVector<IUser>(tUser)
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var targetUser =
            await _queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.TargetUid), default);
        var photos = await _photoAppService.GetPhotosAsync(targetUser);
        var privacies = await _privacyAppService.GetPrivacyListAsync(domainEvent.AggregateEvent.TargetUid);
        var tUser = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent.RequestInfo.UserId, targetUser!, photos, null, privacies);

        var r = new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0,
            Updates = new TVector<IUpdate>(new TUpdatePeerSettings
            {
                Peer = new TPeerUser { UserId = domainEvent.AggregateEvent.TargetUid },
                Settings = new TPeerSettings()
            }),
            Users = new TVector<IUser>(tUser)
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(
        IDomainEvent<ImportContactsSaga, ImportContactsSagaId, ImportContactsCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var importedContacts = domainEvent.AggregateEvent.PhoneContacts
            .Where(p => p.UserId > 0)
            .Select(p => new TImportedContact { ClientId = p.ClientId, UserId = p.UserId }).ToList();
        var userIds = importedContacts.Select(p => p.UserId).ToList();
        var userReadModels =
            await _queryProcessor.ProcessAsync(
                new GetUsersByUidListQuery(userIds), cancellationToken);
        var photoReadModels = await _photoAppService.GetPhotosAsync(userReadModels);
        var privacyReadModels = await _privacyAppService.GetPrivacyListAsync(userIds);
        var contactReadModels =
            await _queryProcessor.ProcessAsync(new GetContactListQuery(domainEvent.AggregateEvent.RequestInfo.UserId,
                userIds), cancellationToken);

        var userList = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToUserList(
            domainEvent.AggregateEvent.RequestInfo.UserId, userReadModels, photoReadModels, contactReadModels,
            privacyReadModels);

        var r = new TImportedContacts
        {
            Imported = new TVector<IImportedContact>(importedContacts),
            PopularInvites = new TVector<IPopularContact>(),
            RetryContacts = new TVector<long>(),
            Users = new TVector<IUser>(userList)
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactProfilePhotoChangedEvent> domainEvent, CancellationToken cancellationToken)
    {
        var userReadModel =
            await _queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.TargetUserId), cancellationToken);
        var privacyList = await _privacyAppService.GetPrivacyListAsync(domainEvent.AggregateEvent.TargetUserId);
        var photoReadModels = await _photoAppService.GetPhotosAsync(userReadModel);
        var photoReadModel = await _photoAppService.GetPhotoAsync(domainEvent.AggregateEvent.PhotoId);
        var contactReadModel =
            await _queryProcessor.ProcessAsync(new GetContactQuery(domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.TargetUserId), cancellationToken);

        var user = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent.SelfUserId, userReadModel!, photoReadModels, contactReadModel, privacyList);
        var r = new MyTelegram.Schema.Photos.TPhoto
        {
            Users = new(user),
            Photo = _layeredPhotoService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToPhoto(photoReadModel)
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }
}
