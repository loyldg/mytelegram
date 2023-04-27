// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IAuthorizations : IObject
{
    int AuthorizationTtlDays { get; set; }
    TVector<MyTelegram.Schema.IAuthorization> Authorizations { get; set; }
}
