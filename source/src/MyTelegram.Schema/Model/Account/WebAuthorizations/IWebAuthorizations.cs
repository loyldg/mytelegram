// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IWebAuthorizations : IObject
{
    TVector<MyTelegram.Schema.IWebAuthorization> Authorizations { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
