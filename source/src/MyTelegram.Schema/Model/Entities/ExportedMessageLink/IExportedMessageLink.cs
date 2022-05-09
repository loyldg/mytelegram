// ReSharper disable All

namespace MyTelegram.Schema;

public interface IExportedMessageLink : IObject
{
    string Link { get; set; }
    string Html { get; set; }

}
