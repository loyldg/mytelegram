// ReSharper disable All

namespace MyTelegram.Schema;

public interface IKeyboardButtonRow : IObject
{
    TVector<Schema.IKeyboardButton> Buttons { get; set; }
}
