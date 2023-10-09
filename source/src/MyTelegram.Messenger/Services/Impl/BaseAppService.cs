namespace MyTelegram.Messenger.Services.Impl;

public class BaseAppService
{
    protected static int CurrentDate => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}
