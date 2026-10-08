// ReSharper disable All
namespace MyTelegram.Schema;

public partial interface ILayeredDialog : IDialog
{
    MyTelegram.Schema.IDraftMessage? Draft { get; set; }
}