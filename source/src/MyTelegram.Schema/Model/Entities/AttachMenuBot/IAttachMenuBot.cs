// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAttachMenuBot : IObject
{
    BitArray Flags { get; set; }
    bool Inactive { get; set; }
    long BotId { get; set; }
    string ShortName { get; set; }
    TVector<MyTelegram.Schema.IAttachMenuBotIcon> Icons { get; set; }

}
