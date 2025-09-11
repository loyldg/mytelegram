// ReSharper disable All
namespace MyTelegram.Schema;

public interface ILayeredDialog : IDialog
{
    MyTelegram.Schema.IDraftMessage? Draft { get; set; }
}