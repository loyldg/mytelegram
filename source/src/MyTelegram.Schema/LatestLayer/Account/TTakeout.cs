﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Takeout info
/// See <a href="https://corefork.telegram.org/constructor/account.takeout" />
///</summary>
[TlObject(0x4dba4501)]
public sealed class TTakeout : ITakeout
{
    public uint ConstructorId => 0x4dba4501;
    ///<summary>
    /// Takeout ID
    ///</summary>
    public long Id { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Id);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Id = reader.ReadInt64();
    }
}