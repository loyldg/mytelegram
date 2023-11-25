// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/payments.CheckedGiftCode" />
///</summary>
[JsonDerivedType(typeof(TCheckedGiftCode), nameof(TCheckedGiftCode))]
public interface ICheckedGiftCode : IObject
{
    BitArray Flags { get; set; }
    bool ViaGiveaway { get; set; }
    MyTelegram.Schema.IPeer FromId { get; set; }
    int? GiveawayMsgId { get; set; }
    long? ToId { get; set; }
    int Date { get; set; }
    int Months { get; set; }
    int? UsedDate { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
