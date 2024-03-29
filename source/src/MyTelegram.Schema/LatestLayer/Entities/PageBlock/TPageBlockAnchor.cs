﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Link to section within the page itself (like <code>&lt;a href="#target"&gt;anchor&lt;/a&gt;</code>)
/// See <a href="https://corefork.telegram.org/constructor/pageBlockAnchor" />
///</summary>
[TlObject(0xce0d37b0)]
public sealed class TPageBlockAnchor : IPageBlock
{
    public uint ConstructorId => 0xce0d37b0;
    ///<summary>
    /// Name of target section
    ///</summary>
    public string Name { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Name);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Name = reader.ReadString();
    }
}