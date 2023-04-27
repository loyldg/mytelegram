// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputClientProxy : IObject
{
    string Address { get; set; }
    int Port { get; set; }
}
