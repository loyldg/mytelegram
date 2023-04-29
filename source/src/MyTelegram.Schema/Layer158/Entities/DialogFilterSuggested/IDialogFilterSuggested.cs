// ReSharper disable All

namespace MyTelegram.Schema;

public interface IDialogFilterSuggested : IObject
{
    Schema.IDialogFilter Filter { get; set; }
    string Description { get; set; }
}
