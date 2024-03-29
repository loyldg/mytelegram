﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains information about a single <a href="https://corefork.telegram.org/api/boost">boost slot »</a>.
/// See <a href="https://corefork.telegram.org/constructor/myBoost" />
///</summary>
[TlObject(0xc448415c)]
public sealed class TMyBoost : IMyBoost
{
    public uint ConstructorId => 0xc448415c;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/boost">Boost slot ID »</a>
    ///</summary>
    public int Slot { get; set; }

    ///<summary>
    /// If set, indicates this slot is currently occupied, i.e. we are <a href="https://corefork.telegram.org/api/boost">boosting</a> this peer.  <br>Note that we can assign multiple boost slots to the same peer.
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    public MyTelegram.Schema.IPeer? Peer { get; set; }

    ///<summary>
    /// When (unixtime) we started boosting the <code>peer</code>, <code>0</code> otherwise.
    ///</summary>
    public int Date { get; set; }

    ///<summary>
    /// Indicates the (unixtime) expiration date of the boost in <code>peer</code> (<code>0</code> if <code>peer</code> is not set).
    ///</summary>
    public int Expires { get; set; }

    ///<summary>
    /// If <code>peer</code> is set, indicates the (unixtime) date after which this boost can be reassigned to another channel.
    ///</summary>
    public int? CooldownUntilDate { get; set; }

    public void ComputeFlag()
    {
        if (Peer != null) { Flags[0] = true; }
        if (/*CooldownUntilDate != 0 && */CooldownUntilDate.HasValue) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Slot);
        if (Flags[0]) { writer.Write(Peer); }
        writer.Write(Date);
        writer.Write(Expires);
        if (Flags[1]) { writer.Write(CooldownUntilDate.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Slot = reader.ReadInt32();
        if (Flags[0]) { Peer = reader.Read<MyTelegram.Schema.IPeer>(); }
        Date = reader.ReadInt32();
        Expires = reader.ReadInt32();
        if (Flags[1]) { CooldownUntilDate = reader.ReadInt32(); }
    }
}