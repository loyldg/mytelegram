namespace MyTelegram.Core;

public static class MyTelegramServerConsts
{
    public const int ProcessRequestTooSlowMilliseconds = 300;

    public const string DbTablePrefix = "App";

    //public const string DbSchema = null;

    public const string Dh2048P = "TG_Dh2048P";
    public const string RsaPublicKey = "TG_RsaPublicKey";
    public const string RsaPrivateKey = "TG_RsaPrivateKey";
    public const string RelayServerIpV4 = "TG_System_RelayServer_IpV4";
    public const string RelayServerIpV6 = "TG_System_RelayServer_IpV6";
    public const string RelayServerPort = "TG_System_RelayServer_Port";
    public const string JoinChatDomain = "TG_System_Chat_JoinChatDomain";
    public const string DcOptionsIpAddress = "TG_System_DcOptions_IpAddress";
    public const string DcOptionsPort = "TG_System_DcOptions_Port";
    public const string UploadRootPath = "TG_System_Upload_RootPath";
    public const string SmsFailedCountPerDay = "TG_System_Sms_Failed_Count_Per_Day";

    public const string RabbitMqConnectionsDefaultHostName = "TG_RabbitMq_Connections_Default_HostName";
    public const string RabbitMqConnectionsDefaultPort = "TG_RabbitMq_Connections_Default_Port";
    public const string RabbitMqConnectionsDefaultUserName = "TG_RabbitMq_Connections_Default_UserName";
    public const string RabbitMqConnectionsDefaultPassword = "TG_RabbitMq_Connections_Default_Password";
    public const string EventBusClientName = "TG_EventBus_ClientName";
    public const string EventBusExchangeName = "TG_EventBus_ExchangeName";
    public const string RedisConfiguration = "TG_Redis_Configuration";
    public const string GRpcIdGeneratorServerUrl = "TG_GRpcIdGeneratorServerUrl";
    public const string GrpcFileServerUrl = "TG_GRpcFileServerUrl";

    public const string MaxChatCount = "TG_MaxChatCount";
    public const string MaxChannelCount = "TG_MaxChannelCount";
    public const string ChannelGetDifferenceIntervalSeconds = "TG_ChannelGetDifferenceIntervalSeconds";

    public const string PhoneCallUserName = "TG_PhoneCallUserName";
    public const string PhoneCallPassword = "TG_PhoneCallPassword";
}