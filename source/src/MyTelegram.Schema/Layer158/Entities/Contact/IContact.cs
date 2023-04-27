// ReSharper disable All

namespace MyTelegram.Schema;

public interface IContact : IObject
{
    long UserId { get; set; }
    bool Mutual { get; set; }
}
