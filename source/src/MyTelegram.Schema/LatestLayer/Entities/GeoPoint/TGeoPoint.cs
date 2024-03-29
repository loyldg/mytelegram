﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// GeoPoint.
/// See <a href="https://corefork.telegram.org/constructor/geoPoint" />
///</summary>
[TlObject(0xb2a2f663)]
public sealed class TGeoPoint : IGeoPoint
{
    public uint ConstructorId => 0xb2a2f663;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Longitude
    ///</summary>
    public double Long { get; set; }

    ///<summary>
    /// Latitude
    ///</summary>
    public double Lat { get; set; }

    ///<summary>
    /// Access hash
    ///</summary>
    public long AccessHash { get; set; }

    ///<summary>
    /// The estimated horizontal accuracy of the location, in meters; as defined by the sender.
    ///</summary>
    public int? AccuracyRadius { get; set; }

    public void ComputeFlag()
    {
        if (/*AccuracyRadius != 0 && */AccuracyRadius.HasValue) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Long);
        writer.Write(Lat);
        writer.Write(AccessHash);
        if (Flags[0]) { writer.Write(AccuracyRadius.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Long = reader.ReadDouble();
        Lat = reader.ReadDouble();
        AccessHash = reader.ReadInt64();
        if (Flags[0]) { AccuracyRadius = reader.ReadInt32(); }
    }
}