﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Game highscore
/// See <a href="https://corefork.telegram.org/constructor/highScore" />
///</summary>
[TlObject(0x73a379eb)]
public sealed class THighScore : IHighScore
{
    public uint ConstructorId => 0x73a379eb;
    ///<summary>
    /// Position in highscore list
    ///</summary>
    public int Pos { get; set; }

    ///<summary>
    /// User ID
    ///</summary>
    public long UserId { get; set; }

    ///<summary>
    /// Score
    ///</summary>
    public int Score { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Pos);
        writer.Write(UserId);
        writer.Write(Score);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Pos = reader.ReadInt32();
        UserId = reader.ReadInt64();
        Score = reader.ReadInt32();
    }
}