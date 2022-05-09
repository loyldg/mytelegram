// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPopularContact : IObject
{
    long ClientId { get; set; }
    int Importers { get; set; }

}
