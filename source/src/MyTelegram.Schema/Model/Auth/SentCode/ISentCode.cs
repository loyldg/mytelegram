// ReSharper disable All

namespace MyTelegram.Schema.Auth;

public interface ISentCode : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.Auth.ISentCodeType Type { get; set; }
    string PhoneCodeHash { get; set; }
    MyTelegram.Schema.Auth.ICodeType? NextType { get; set; }
    int? Timeout { get; set; }

}
