// ReSharper disable All

namespace MyTelegram.Schema;

public interface IKeyboardButtonRow : IObject
{
    TVector<MyTelegram.Schema.IKeyboardButton> Buttons { get; set; }
}
