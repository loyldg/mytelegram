// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPageCaption : IObject
{
    Schema.IRichText Text { get; set; }
    Schema.IRichText Credit { get; set; }
}
