// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputGroupCall : IObject
{
    long Id { get; set; }
    long AccessHash { get; set; }

}
