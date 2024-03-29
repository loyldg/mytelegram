﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Chunk of a livestream
/// See <a href="https://corefork.telegram.org/constructor/inputGroupCallStream" />
///</summary>
[TlObject(0x598a92a)]
public sealed class TInputGroupCallStream : IInputFileLocation
{
    public uint ConstructorId => 0x598a92a;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Livestream info
    /// See <a href="https://corefork.telegram.org/type/InputGroupCall" />
    ///</summary>
    public MyTelegram.Schema.IInputGroupCall Call { get; set; }

    ///<summary>
    /// Timestamp in milliseconds
    ///</summary>
    public long TimeMs { get; set; }

    ///<summary>
    /// Specifies the duration of the video segment to fetch in milliseconds, by bitshifting <code>1000</code> to the right <code>scale</code> times: <code>duration_ms := 1000 &gt;&gt; scale</code>
    ///</summary>
    public int Scale { get; set; }

    ///<summary>
    /// Selected video channel
    ///</summary>
    public int? VideoChannel { get; set; }

    ///<summary>
    /// Selected video quality (0 = lowest, 1 = medium, 2 = best)
    ///</summary>
    public int? VideoQuality { get; set; }

    public void ComputeFlag()
    {
        if (/*VideoChannel != 0 && */VideoChannel.HasValue) { Flags[0] = true; }
        if (/*VideoQuality != 0 && */VideoQuality.HasValue) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Call);
        writer.Write(TimeMs);
        writer.Write(Scale);
        if (Flags[0]) { writer.Write(VideoChannel.Value); }
        if (Flags[0]) { writer.Write(VideoQuality.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Call = reader.Read<MyTelegram.Schema.IInputGroupCall>();
        TimeMs = reader.ReadInt64();
        Scale = reader.ReadInt32();
        if (Flags[0]) { VideoChannel = reader.ReadInt32(); }
        if (Flags[0]) { VideoQuality = reader.ReadInt32(); }
    }
}