// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAttachMenuBotIcon : IObject
{
    BitArray Flags { get; set; }
    string Name { get; set; }
    MyTelegram.Schema.IDocument Icon { get; set; }
    TVector<MyTelegram.Schema.IAttachMenuBotIconColor>? Colors { get; set; }
}
