// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

public interface IImportedContacts : IObject
{
    TVector<MyTelegram.Schema.IImportedContact> Imported { get; set; }
    TVector<MyTelegram.Schema.IPopularContact> PopularInvites { get; set; }
    TVector<long> RetryContacts { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
