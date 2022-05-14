﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
///See <a href="https://core.telegram.org/method/messages.exportChatInvite" />
///</summary>
[TlObject(0xa02ce5d5)]
public sealed class RequestExportChatInvite : IRequest<MyTelegram.Schema.IExportedChatInvite>
{
    public uint ConstructorId => 0xa02ce5d5;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool LegacyRevokePermanent { get; set; }
    public bool RequestNeeded { get; set; }

    ///<summary>
    ///See <a href="https://core.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }
    public int? ExpireDate { get; set; }
    public int? UsageLimit { get; set; }
    public string? Title { get; set; }

    public void ComputeFlag()
    {
        if (LegacyRevokePermanent) { Flags[2] = true; }
        if (RequestNeeded) { Flags[3] = true; }
        if (ExpireDate != 0 && ExpireDate.HasValue) { Flags[0] = true; }
        if (UsageLimit != 0 && UsageLimit.HasValue) { Flags[1] = true; }
        if (Title != null) { Flags[4] = true; }
    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        bw.Serialize(Flags);
        Peer.Serialize(bw);
        if (Flags[0]) { bw.Write(ExpireDate.Value); }
        if (Flags[1]) { bw.Write(UsageLimit.Value); }
        if (Flags[4]) { bw.Serialize(Title); }
    }

    public void Deserialize(BinaryReader br)
    {
        Flags = br.Deserialize<BitArray>();
        if (Flags[2]) { LegacyRevokePermanent = true; }
        if (Flags[3]) { RequestNeeded = true; }
        Peer = br.Deserialize<MyTelegram.Schema.IInputPeer>();
        if (Flags[0]) { ExpireDate = br.ReadInt32(); }
        if (Flags[1]) { UsageLimit = br.ReadInt32(); }
        if (Flags[4]) { Title = br.Deserialize<string>(); }
    }
}
