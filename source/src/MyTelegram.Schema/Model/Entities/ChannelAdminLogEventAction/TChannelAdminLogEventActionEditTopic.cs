﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/channelAdminLogEventActionEditTopic" />
///</summary>
[TlObject(0xf06fe208)]
public class TChannelAdminLogEventActionEditTopic : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0xf06fe208;

    ///<summary>
    ///See <a href="https://core.telegram.org/type/ForumTopic" />
    ///</summary>
    public MyTelegram.Schema.IForumTopic PrevTopic { get; set; }

    ///<summary>
    ///See <a href="https://core.telegram.org/type/ForumTopic" />
    ///</summary>
    public MyTelegram.Schema.IForumTopic NewTopic { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        PrevTopic.Serialize(bw);
        NewTopic.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        PrevTopic = br.Deserialize<MyTelegram.Schema.IForumTopic>();
        NewTopic = br.Deserialize<MyTelegram.Schema.IForumTopic>();
    }
}