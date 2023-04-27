// ReSharper disable All

namespace MyTelegram.Schema.Bots;

public interface IBotInfo : IObject
{
    string Name { get; set; }
    string About { get; set; }
    string Description { get; set; }
}
