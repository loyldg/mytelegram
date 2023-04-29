// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IAuthorizations : IObject
{
    int AuthorizationTtlDays { get; set; }
    TVector<Schema.IAuthorization> Authorizations { get; set; }
}
