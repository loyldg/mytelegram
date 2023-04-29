// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAttachMenuBotIcon : IObject
{
    BitArray Flags { get; set; }
    string Name { get; set; }
    Schema.IDocument Icon { get; set; }
    TVector<Schema.IAttachMenuBotIconColor>? Colors { get; set; }
}
