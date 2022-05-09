namespace MyTelegram.Core;

public class AppSettingManager : IAppSettingManager//, ISingletonDependency
{
    private static readonly Dictionary<string, string> Settings = new()
    {
        { MyTelegramServerConsts.DcOptionsIpAddress, "192.168.2.121" },
        { MyTelegramServerConsts.DcOptionsPort, "20443" },

        { MyTelegramServerConsts.RelayServerIpV4, "192.168.2.121" },
        { MyTelegramServerConsts.RelayServerPort, "20444" },
        { MyTelegramServerConsts.JoinChatDomain, "https://t.xx/joinChat/" },
        { MyTelegramServerConsts.UploadRootPath, "" },
        { MyTelegramServerConsts.SmsFailedCountPerDay, "20" },
        { MyTelegramServerConsts.GRpcIdGeneratorServerUrl, "http://localhost:5000" },
        { MyTelegramServerConsts.GrpcFileServerUrl, "http://localhost:5002" },
        { MyTelegramServerConsts.MaxChatCount, "5" },
        { MyTelegramServerConsts.MaxChannelCount, "5" },
        { MyTelegramServerConsts.ChannelGetDifferenceIntervalSeconds, "120" },
        { MyTelegramServerConsts.PhoneCallUserName, "a" },
        { MyTelegramServerConsts.PhoneCallPassword, "b" },
    };

    //private readonly IRepository<TgConfig> _tgConfigRepository;
    //private readonly ILogger<AppSettingManager> _logger;



    public string GetSetting(string key)
    {
        if (Settings.ContainsKey(key))
        {
            return Settings[key];
        }

        return string.Empty;
    }

    public int GetIntSetting(string key)
    {
        var v = GetSetting(key);
        if (int.TryParse(v, out var value))
        {
            return value;
        }

        return 0;
    }
}