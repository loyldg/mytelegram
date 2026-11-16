namespace MyTelegram.MTProto.Tests;

public class ClientData : IClientData
{
    public ProtocolType MtProtoType { get; set; }
    public bool IsFirstPacketParsed { get; set; }
    public byte[] SendKey { get; set; } = [];
    public byte[] ReceiveKey { get; set; } = [];
    public long AuthKeyId { get; set; }
    public string ConnectionId { get; set; } = null!;
    public bool ObfuscationEnabled { get; set; }
    public int CurrentPacketLength { get; set; }
    public int SkipCount { get; set; }
    public byte[] SendIv { get; set; } = [];
    public byte[] ReceiveIv { get; set; } = [];
    public ulong ReceiveCount { get; set; }
    public ulong SendCount { get; set; }
    public MtpTransportState TransportState { get; set; }
    public ArrayPool<byte> ArrayPool { get; set; } = ArrayPool<byte>.Shared;
}