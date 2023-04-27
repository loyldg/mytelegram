// ReSharper disable All

namespace MyTelegram.Schema;

public interface IExportedContactToken : IObject
{
    string Url { get; set; }
    int Expires { get; set; }
}
