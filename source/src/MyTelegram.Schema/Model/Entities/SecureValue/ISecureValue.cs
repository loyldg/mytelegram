// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISecureValue : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.ISecureValueType Type { get; set; }
    MyTelegram.Schema.ISecureData? Data { get; set; }
    MyTelegram.Schema.ISecureFile? FrontSide { get; set; }
    MyTelegram.Schema.ISecureFile? ReverseSide { get; set; }
    MyTelegram.Schema.ISecureFile? Selfie { get; set; }
    TVector<MyTelegram.Schema.ISecureFile>? Translation { get; set; }
    TVector<MyTelegram.Schema.ISecureFile>? Files { get; set; }
    MyTelegram.Schema.ISecurePlainData? PlainData { get; set; }
    byte[] Hash { get; set; }

}
