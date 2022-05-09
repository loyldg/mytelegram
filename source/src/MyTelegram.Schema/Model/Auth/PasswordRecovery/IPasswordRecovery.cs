// ReSharper disable All

namespace MyTelegram.Schema.Auth;

public interface IPasswordRecovery : IObject
{
    string EmailPattern { get; set; }

}
