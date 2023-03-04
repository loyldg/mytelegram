// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAutoSaveException : IObject
{
    MyTelegram.Schema.IPeer Peer { get; set; }
    MyTelegram.Schema.IAutoSaveSettings Settings { get; set; }
}
