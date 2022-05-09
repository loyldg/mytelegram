// ReSharper disable All

namespace MyTelegram.Schema;

public interface IDialogFilterSuggested : IObject
{
    MyTelegram.Schema.IDialogFilter Filter { get; set; }
    string Description { get; set; }

}
