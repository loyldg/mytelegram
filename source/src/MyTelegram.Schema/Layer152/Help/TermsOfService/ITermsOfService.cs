// ReSharper disable All

namespace MyTelegram.Schema.Help;

public interface ITermsOfService : IObject
{
    BitArray Flags { get; set; }
    bool Popup { get; set; }
    MyTelegram.Schema.IDataJSON Id { get; set; }
    string Text { get; set; }
    TVector<MyTelegram.Schema.IMessageEntity> Entities { get; set; }
    int? MinAgeConfirm { get; set; }
}
