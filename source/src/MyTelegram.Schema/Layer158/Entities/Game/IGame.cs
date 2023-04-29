// ReSharper disable All

namespace MyTelegram.Schema;

public interface IGame : IObject
{
    BitArray Flags { get; set; }
    long Id { get; set; }
    long AccessHash { get; set; }
    string ShortName { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    Schema.IPhoto Photo { get; set; }
    Schema.IDocument? Document { get; set; }
}
