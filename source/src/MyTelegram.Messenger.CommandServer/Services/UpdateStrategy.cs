namespace MyTelegram.Messenger.CommandServer.Services;

public enum UpdateStrategy
{
    None,
    All,
    UpdateDatabase,
    UpdateCache,
}