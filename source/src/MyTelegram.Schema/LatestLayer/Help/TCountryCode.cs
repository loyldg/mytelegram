﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Country code and phone number pattern of a specific country
/// See <a href="https://corefork.telegram.org/constructor/help.countryCode" />
///</summary>
[TlObject(0x4203c5ef)]
public sealed class TCountryCode : ICountryCode
{
    public uint ConstructorId => 0x4203c5ef;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// ISO country code
    ///</summary>
    public string CountryCode { get; set; }

    ///<summary>
    /// Possible phone prefixes
    ///</summary>
    public TVector<string>? Prefixes { get; set; }

    ///<summary>
    /// Phone patterns: for example, <code>XXX XXX XXX</code>
    ///</summary>
    public TVector<string>? Patterns { get; set; }

    public void ComputeFlag()
    {
        if (Prefixes?.Count > 0) { Flags[0] = true; }
        if (Patterns?.Count > 0) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(CountryCode);
        if (Flags[0]) { writer.Write(Prefixes); }
        if (Flags[1]) { writer.Write(Patterns); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        CountryCode = reader.ReadString();
        if (Flags[0]) { Prefixes = reader.Read<TVector<string>>(); }
        if (Flags[1]) { Patterns = reader.Read<TVector<string>>(); }
    }
}