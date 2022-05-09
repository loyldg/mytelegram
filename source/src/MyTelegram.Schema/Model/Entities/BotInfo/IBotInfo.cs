// ReSharper disable All

namespace MyTelegram.Schema;

public interface IBotInfo : IObject
{
    long UserId { get; set; }
    string Description { get; set; }
    TVector<MyTelegram.Schema.IBotCommand> Commands { get; set; }

}
