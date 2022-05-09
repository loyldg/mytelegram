// ReSharper disable All

namespace MyTelegram.Schema;

public interface IChatInviteImporter : IObject
{
    long UserId { get; set; }
    int Date { get; set; }

}
