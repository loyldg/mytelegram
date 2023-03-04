// ReSharper disable All

namespace MyTelegram.Schema;

public interface IBotInfo : IObject
{
    BitArray Flags { get; set; }
    long? UserId { get; set; }
    string? Description { get; set; }
    MyTelegram.Schema.IPhoto? DescriptionPhoto { get; set; }
    MyTelegram.Schema.IDocument? DescriptionDocument { get; set; }
    TVector<MyTelegram.Schema.IBotCommand>? Commands { get; set; }
    MyTelegram.Schema.IBotMenuButton? MenuButton { get; set; }
}
