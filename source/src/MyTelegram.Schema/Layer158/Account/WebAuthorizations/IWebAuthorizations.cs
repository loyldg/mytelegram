// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IWebAuthorizations : IObject
{
    TVector<Schema.IWebAuthorization> Authorizations { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
