﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents an issue with the translated version of a document. The error is considered resolved when a file with the document translation changes.
/// See <a href="https://corefork.telegram.org/constructor/secureValueErrorTranslationFiles" />
///</summary>
[TlObject(0x34636dd8)]
public sealed class TSecureValueErrorTranslationFiles : ISecureValueError
{
    public uint ConstructorId => 0x34636dd8;
    ///<summary>
    /// One of <a href="https://corefork.telegram.org/constructor/secureValueTypePersonalDetails">secureValueTypePersonalDetails</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypePassport">secureValueTypePassport</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeDriverLicense">secureValueTypeDriverLicense</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeIdentityCard">secureValueTypeIdentityCard</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeInternalPassport">secureValueTypeInternalPassport</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeUtilityBill">secureValueTypeUtilityBill</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeBankStatement">secureValueTypeBankStatement</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeRentalAgreement">secureValueTypeRentalAgreement</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypePassportRegistration">secureValueTypePassportRegistration</a>, <a href="https://corefork.telegram.org/constructor/secureValueTypeTemporaryRegistration">secureValueTypeTemporaryRegistration</a>
    /// See <a href="https://corefork.telegram.org/type/SecureValueType" />
    ///</summary>
    public MyTelegram.Schema.ISecureValueType Type { get; set; }

    ///<summary>
    /// Hash
    ///</summary>
    public TVector<byte[]> FileHash { get; set; }

    ///<summary>
    /// Error message
    ///</summary>
    public string Text { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Type);
        writer.Write(FileHash);
        writer.Write(Text);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Type = reader.Read<MyTelegram.Schema.ISecureValueType>();
        FileHash = reader.Read<TVector<byte[]>>();
        Text = reader.ReadString();
    }
}