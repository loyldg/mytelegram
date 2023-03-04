// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPageCaption : IObject
{
    MyTelegram.Schema.IRichText Text { get; set; }
    MyTelegram.Schema.IRichText Credit { get; set; }
}
