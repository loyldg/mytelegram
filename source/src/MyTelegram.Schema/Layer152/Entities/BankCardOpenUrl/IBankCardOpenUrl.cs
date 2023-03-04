// ReSharper disable All

namespace MyTelegram.Schema;

public interface IBankCardOpenUrl : IObject
{
    string Url { get; set; }
    string Name { get; set; }
}
