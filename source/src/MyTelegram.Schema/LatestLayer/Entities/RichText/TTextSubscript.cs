﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Subscript text
/// See <a href="https://corefork.telegram.org/constructor/textSubscript" />
///</summary>
[TlObject(0xed6a8504)]
public sealed class TTextSubscript : IRichText
{
    public uint ConstructorId => 0xed6a8504;
    ///<summary>
    /// Text
    /// See <a href="https://corefork.telegram.org/type/RichText" />
    ///</summary>
    public MyTelegram.Schema.IRichText Text { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Text);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Text = reader.Read<MyTelegram.Schema.IRichText>();
    }
}