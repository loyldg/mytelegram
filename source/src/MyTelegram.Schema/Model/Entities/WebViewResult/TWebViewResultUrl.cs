﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/webViewResultUrl" />
///</summary>
[TlObject(0xc14557c)]
public class TWebViewResultUrl : IWebViewResult
{
    public uint ConstructorId => 0xc14557c;
    public long QueryId { get; set; }
    public string Url { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        bw.Write(QueryId);
        bw.Serialize(Url);
    }

    public void Deserialize(BinaryReader br)
    {
        QueryId = br.ReadInt64();
        Url = br.Deserialize<string>();
    }
}