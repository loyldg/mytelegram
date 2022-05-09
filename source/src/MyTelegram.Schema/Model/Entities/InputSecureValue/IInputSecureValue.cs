// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputSecureValue : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.ISecureValueType Type { get; set; }
    MyTelegram.Schema.ISecureData? Data { get; set; }
    MyTelegram.Schema.IInputSecureFile? FrontSide { get; set; }
    MyTelegram.Schema.IInputSecureFile? ReverseSide { get; set; }
    MyTelegram.Schema.IInputSecureFile? Selfie { get; set; }
    TVector<MyTelegram.Schema.IInputSecureFile>? Translation { get; set; }
    TVector<MyTelegram.Schema.IInputSecureFile>? Files { get; set; }
    MyTelegram.Schema.ISecurePlainData? PlainData { get; set; }

}
