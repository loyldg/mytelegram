// ReSharper disable All

namespace MyTelegram.Schema;

public interface IGroupCall : IObject
{
    long Id { get; set; }
    long AccessHash { get; set; }
}
