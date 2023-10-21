namespace MyTelegram.EventFlow.MongoDB;

public enum UpdateStrategy
{
    None,
    All,
    UpdateDatabase,
    UpdateCache,
}