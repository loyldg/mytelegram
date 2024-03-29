﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Required type
/// See <a href="https://corefork.telegram.org/constructor/secureRequiredType" />
///</summary>
[TlObject(0x829d99da)]
public sealed class TSecureRequiredType : ISecureRequiredType
{
    public uint ConstructorId => 0x829d99da;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Native names
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool NativeNames { get; set; }

    ///<summary>
    /// Is a selfie required
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool SelfieRequired { get; set; }

    ///<summary>
    /// Is a translation required
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool TranslationRequired { get; set; }

    ///<summary>
    /// Secure value type
    /// See <a href="https://corefork.telegram.org/type/SecureValueType" />
    ///</summary>
    public MyTelegram.Schema.ISecureValueType Type { get; set; }

    public void ComputeFlag()
    {
        if (NativeNames) { Flags[0] = true; }
        if (SelfieRequired) { Flags[1] = true; }
        if (TranslationRequired) { Flags[2] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Type);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { NativeNames = true; }
        if (Flags[1]) { SelfieRequired = true; }
        if (Flags[2]) { TranslationRequired = true; }
        Type = reader.Read<MyTelegram.Schema.ISecureValueType>();
    }
}