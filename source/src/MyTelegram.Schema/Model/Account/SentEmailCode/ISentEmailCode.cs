// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface ISentEmailCode : IObject
{
    string EmailPattern { get; set; }
    int Length { get; set; }

}
