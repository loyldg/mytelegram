﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Emoji keyword
/// See <a href="https://corefork.telegram.org/constructor/emojiKeyword" />
///</summary>
[TlObject(0xd5b3b9f9)]
public sealed class TEmojiKeyword : IEmojiKeyword
{
    public uint ConstructorId => 0xd5b3b9f9;
    ///<summary>
    /// Keyword
    ///</summary>
    public string Keyword { get; set; }

    ///<summary>
    /// Emojis associated to keyword
    ///</summary>
    public TVector<string> Emoticons { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Keyword);
        writer.Write(Emoticons);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Keyword = reader.ReadString();
        Emoticons = reader.Read<TVector<string>>();
    }
}