namespace MyTelegram.Messenger;
#nullable disable
public class MyTelegramMessengerServerOptions
{
    public string FileServerAppId { get; set; }
    public string FileServerGrpcServiceUrl { get; set; }
    public string IdGeneratorGrpcServiceUrl { get; set; }
    public string MessengerServerGrpcServiceUrl { get; set; }
    public int FixedVerifyCode { get; set; }
    public int ConfirmEmailLoginCount { get; set; }
    public string JoinChatDomain { get; set; }
    public int ChannelGetDifferenceIntervalSeconds { get; set; }

    public bool UseInMemoryFilters { get; set; }
    public int BlockedCountMax { get; set; }

    public int ChatSizeMax { get; set; }
    public int MegagroupSizeMax { get; set; }
    public int EditTimeLimit { get; set; }
    public int ForwardedCountMax { get; set; }
    public int PinnedDialogsCountMax { get; set; }
    public int PinnedInfolderCountMax { get; set; }
    public int CaptionLengthMax { get; set; }
    public int MessageLengthMax { get; set; }

    public bool IsMediaDc { get; set; }
    public string UploadRootPath { get; set; }
    public List<WebRtcConnection> WebRtcConnections { get; set; }
    public int ThisDcId { get; set; }
    public List<DcOption> DcOptions { get; set; }
    public bool AutoCreateSuperGroup { get; set; }
    public bool EnableFutureAuthToken { get; set; }
}