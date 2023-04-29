// ReSharper disable All

namespace MyTelegram.Schema;

public interface IBotInfo : IObject
{
    BitArray Flags { get; set; }
    long? UserId { get; set; }
    string? Description { get; set; }
    Schema.IPhoto? DescriptionPhoto { get; set; }
    Schema.IDocument? DescriptionDocument { get; set; }
    TVector<Schema.IBotCommand>? Commands { get; set; }
    Schema.IBotMenuButton? MenuButton { get; set; }
}
