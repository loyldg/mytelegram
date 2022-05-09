// ReSharper disable All

namespace MyTelegram.Schema;

public interface IWallPaper : IObject
{
    long Id { get; set; }
    BitArray Flags { get; set; }
    bool Default { get; set; }
    bool Dark { get; set; }
    MyTelegram.Schema.IWallPaperSettings? Settings { get; set; }

}
