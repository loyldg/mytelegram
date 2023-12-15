﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Get highscores of a game
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/messages.getGameHighScores" />
///</summary>
[TlObject(0xe822649d)]
public sealed class RequestGetGameHighScores : IRequest<MyTelegram.Schema.Messages.IHighScores>
{
    public uint ConstructorId => 0xe822649d;
    ///<summary>
    /// Where was the game sent
    /// See <a href="https://corefork.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }

    ///<summary>
    /// ID of message with game media attachment
    ///</summary>
    public int Id { get; set; }

    ///<summary>
    /// Get high scores made by a certain user
    /// See <a href="https://corefork.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser UserId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Peer);
        writer.Write(Id);
        writer.Write(UserId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Peer = reader.Read<MyTelegram.Schema.IInputPeer>();
        Id = reader.ReadInt32();
        UserId = reader.Read<MyTelegram.Schema.IInputUser>();
    }
}