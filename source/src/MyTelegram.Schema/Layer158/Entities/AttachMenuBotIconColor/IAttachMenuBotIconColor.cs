// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAttachMenuBotIconColor : IObject
{
    string Name { get; set; }
    int Color { get; set; }
}
