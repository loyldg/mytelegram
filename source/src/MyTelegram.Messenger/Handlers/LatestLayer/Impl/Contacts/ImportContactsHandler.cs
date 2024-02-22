// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Imports contacts: saves a full list on the server, adds already registered contacts to the contact list, returns added contacts and their info.Use <a href="https://corefork.telegram.org/method/contacts.addContact">contacts.addContact</a> to add Telegram contacts without actually using their phone number.
/// See <a href="https://corefork.telegram.org/method/contacts.importContacts" />
///</summary>
internal sealed class ImportContactsHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestImportContacts, MyTelegram.Schema.Contacts.IImportedContacts>,
    Contacts.IImportContactsHandler
{
    private readonly ICacheManager<UserCacheItem> _cacheManager;
    private readonly ICommandBus _commandBus;
    private readonly IQueryProcessor _queryProcessor;

    public ImportContactsHandler(
        ICommandBus commandBus,
        ICacheManager<UserCacheItem> cacheManager, IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _cacheManager = cacheManager;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IImportedContacts> HandleCoreAsync(IRequestInput input,
        RequestImportContacts obj)
    {
        if (obj.Contacts.Count == 0)
        {
            //throw new BadRequestException("Contacts required.");
            RpcErrors.RpcErrors400.ContactIdInvalid.ThrowRpcError();
        }

        //var userReadModels=await _queryProcessor.ProcessAsync(new GetUsersByUidListQuery())

        var keys = obj.Contacts.Select(p => UserCacheItem.GetCacheKey(p.Phone)).ToList();
        var userIdDict = await _cacheManager.GetManyAsync(keys);
        var phoneContactList = new List<PhoneContact>();

        var contactDict = obj.Contacts.DistinctBy(k => k.Phone)
            .ToDictionary(k => k.Phone, v => v);
        //var userIdDict = userIdList.ToDictionary(k => k.Key, v => v);
        foreach (var item in obj.Contacts)
        {
            var userId = 0L;
            var key = UserCacheItem.GetCacheKey(item.Phone);
            if (userIdDict.TryGetValue(key, out var cachedUserItem))
            {
                userId = cachedUserItem?.UserId ?? 0;
            }

            phoneContactList.Add(new PhoneContact(userId,
                item.Phone,
                item.FirstName,
                item.LastName,
                item.ClientId));
        }

        var firstContact = phoneContactList.FirstOrDefault();
        //var firstContactUid

        var command = new ImportContactsCommand(ImportedContactId.Create(input.UserId, "-"),
            input.ToRequestInfo(),
            input.UserId,
            phoneContactList);
        await _commandBus.PublishAsync(command, default);

        return null!;
    }
}
