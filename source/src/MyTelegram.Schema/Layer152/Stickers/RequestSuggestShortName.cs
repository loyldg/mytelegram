﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Stickers;

///<summary>
///See <a href="https://core.telegram.org/method/stickers.suggestShortName" />
///</summary>
[TlObject(0x4dafc503)]
public sealed class RequestSuggestShortName : IRequest<MyTelegram.Schema.Stickers.ISuggestedShortName>
{
    public uint ConstructorId => 0x4dafc503;
    public string Title { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        bw.Serialize(Title);
    }

    public void Deserialize(BinaryReader br)
    {
        Title = br.Deserialize<string>();
    }
}