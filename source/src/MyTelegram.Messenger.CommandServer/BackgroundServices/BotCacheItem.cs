namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public record BotCacheItem(long BotUserId, bool AllJoinToGroups, bool AllowAccessGroupMessages, string UserName);
