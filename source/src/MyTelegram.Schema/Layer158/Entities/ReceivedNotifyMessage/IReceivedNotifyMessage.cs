// ReSharper disable All

namespace MyTelegram.Schema;

public interface IReceivedNotifyMessage : IObject
{
    int Id { get; set; }
    int Flags { get; set; }
}
