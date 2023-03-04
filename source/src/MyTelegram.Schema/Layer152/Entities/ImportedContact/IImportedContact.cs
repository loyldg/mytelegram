// ReSharper disable All

namespace MyTelegram.Schema;

public interface IImportedContact : IObject
{
    long UserId { get; set; }
    long ClientId { get; set; }
}
