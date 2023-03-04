// ReSharper disable All

namespace MyTelegram.Schema;

public interface IWebAuthorization : IObject
{
    long Hash { get; set; }
    long BotId { get; set; }
    string Domain { get; set; }
    string Browser { get; set; }
    string Platform { get; set; }
    int DateCreated { get; set; }
    int DateActive { get; set; }
    string Ip { get; set; }
    string Region { get; set; }
}
