// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAutoSaveException : IObject
{
    Schema.IPeer Peer { get; set; }
    Schema.IAutoSaveSettings Settings { get; set; }
}
