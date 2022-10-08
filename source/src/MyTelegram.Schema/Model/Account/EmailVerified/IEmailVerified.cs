// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IEmailVerified : IObject
{
    string Email { get; set; }

}
