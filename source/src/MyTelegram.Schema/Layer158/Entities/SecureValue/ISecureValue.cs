// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISecureValue : IObject
{
    BitArray Flags { get; set; }
    Schema.ISecureValueType Type { get; set; }
    Schema.ISecureData? Data { get; set; }
    Schema.ISecureFile? FrontSide { get; set; }
    Schema.ISecureFile? ReverseSide { get; set; }
    Schema.ISecureFile? Selfie { get; set; }
    TVector<Schema.ISecureFile>? Translation { get; set; }
    TVector<Schema.ISecureFile>? Files { get; set; }
    Schema.ISecurePlainData? PlainData { get; set; }
    byte[] Hash { get; set; }
}
