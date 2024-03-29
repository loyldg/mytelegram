﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The preview of the webpage hasn't changed
/// See <a href="https://corefork.telegram.org/constructor/webPageNotModified" />
///</summary>
[TlObject(0x7311ca11)]
public sealed class TWebPageNotModified : IWebPage
{
    public uint ConstructorId => 0x7311ca11;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Page view count
    ///</summary>
    public int? CachedPageViews { get; set; }

    public void ComputeFlag()
    {
        if (/*CachedPageViews != 0 && */CachedPageViews.HasValue) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        if (Flags[0]) { writer.Write(CachedPageViews.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { CachedPageViews = reader.ReadInt32(); }
    }
}