// ReSharper disable All

namespace MyTelegram.Schema;

public interface IFutureSalt : IObject
{
    int ValidSince { get; set; }
    int ValidUntil { get; set; }
    long Salt { get; set; }
}
