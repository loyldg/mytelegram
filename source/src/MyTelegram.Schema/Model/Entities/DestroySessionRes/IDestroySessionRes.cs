// ReSharper disable All

namespace MyTelegram.Schema;

public interface IDestroySessionRes : IObject
{
    long SessionId { get; set; }

}
