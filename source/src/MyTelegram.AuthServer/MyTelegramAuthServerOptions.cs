namespace MyTelegram.AuthServer;

public class MyTelegramAuthServerOptions
{
    public string PrivateKeyFilePath { get; set; } = string.Empty;
    public int AuthStep1DelayMs { get; set; } = 50;
}