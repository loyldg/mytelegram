// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputSecureValue : IObject
{
    BitArray Flags { get; set; }
    Schema.ISecureValueType Type { get; set; }
    Schema.ISecureData? Data { get; set; }
    Schema.IInputSecureFile? FrontSide { get; set; }
    Schema.IInputSecureFile? ReverseSide { get; set; }
    Schema.IInputSecureFile? Selfie { get; set; }
    TVector<Schema.IInputSecureFile>? Translation { get; set; }
    TVector<Schema.IInputSecureFile>? Files { get; set; }
    Schema.ISecurePlainData? PlainData { get; set; }
}
