﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
///See <a href="https://core.telegram.org/method/messages.editExportedChatInvite" />
///</summary>
[TlObject(0xbdca2f75)]
public sealed class RequestEditExportedChatInvite : IRequest<MyTelegram.Schema.Messages.IExportedChatInvite>
{
    public uint ConstructorId => 0xbdca2f75;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool Revoked { get; set; }

    ///<summary>
    ///See <a href="https://core.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }
    public string Link { get; set; }
    public int? ExpireDate { get; set; }
    public int? UsageLimit { get; set; }

    ///<summary>
    ///See <a href="https://core.telegram.org/type/Bool" />
    ///</summary>
    public bool? RequestNeeded { get; set; }
    public string? Title { get; set; }

    public void ComputeFlag()
    {
        if (Revoked) { Flags[2] = true; }
        if (ExpireDate != 0 && ExpireDate.HasValue) { Flags[0] = true; }
        if (UsageLimit != 0 && UsageLimit.HasValue) { Flags[1] = true; }
        if (RequestNeeded !=null) { Flags[3] = true; }
        if (Title != null) { Flags[4] = true; }
    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        bw.Serialize(Flags);
        Peer.Serialize(bw);
        bw.Serialize(Link);
        if (Flags[0]) { bw.Write(ExpireDate.Value); }
        if (Flags[1]) { bw.Write(UsageLimit.Value); }
        if (Flags[3]) { bw.Serialize(RequestNeeded.Value); }
        if (Flags[4]) { bw.Serialize(Title); }
    }

    public void Deserialize(BinaryReader br)
    {
        Flags = br.Deserialize<BitArray>();
        if (Flags[2]) { Revoked = true; }
        Peer = br.Deserialize<MyTelegram.Schema.IInputPeer>();
        Link = br.Deserialize<string>();
        if (Flags[0]) { ExpireDate = br.ReadInt32(); }
        if (Flags[1]) { UsageLimit = br.ReadInt32(); }
        if (Flags[3]) { RequestNeeded = br.Deserialize<bool>(); }
        if (Flags[4]) { Title = br.Deserialize<string>(); }
    }
}