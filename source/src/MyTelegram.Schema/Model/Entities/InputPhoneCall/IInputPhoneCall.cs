// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputPhoneCall : IObject
{
    long Id { get; set; }
    long AccessHash { get; set; }

}
