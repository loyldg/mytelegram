// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IAuthorizations : IObject
{
    TVector<MyTelegram.Schema.IAuthorization> Authorizations { get; set; }

}
