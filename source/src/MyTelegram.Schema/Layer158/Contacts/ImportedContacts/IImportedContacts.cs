// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

public interface IImportedContacts : IObject
{
    TVector<Schema.IImportedContact> Imported { get; set; }
    TVector<Schema.IPopularContact> PopularInvites { get; set; }
    TVector<long> RetryContacts { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
