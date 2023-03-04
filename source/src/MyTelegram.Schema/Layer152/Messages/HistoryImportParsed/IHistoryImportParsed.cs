// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IHistoryImportParsed : IObject
{
    BitArray Flags { get; set; }
    bool Pm { get; set; }
    bool Group { get; set; }
    string? Title { get; set; }
}
