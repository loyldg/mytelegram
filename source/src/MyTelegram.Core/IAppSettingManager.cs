namespace MyTelegram.Core;

public interface IAppSettingManager
{
    int GetIntSetting(string key);

    string GetSetting(string key);
}