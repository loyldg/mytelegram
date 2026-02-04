using System.Linq;
using System.Text;
using MyTelegram.Messenger.DomainEventHandlers;
using MyTelegram.Schema;
using EventFlow.Aggregates;
using EventFlow.Commands;
using EventFlow.Queries;
using MyTelegram.Domain.Aggregates.Messaging;
using MyTelegram.Domain.Aggregates.User;
using MyTelegram.Queries;
using MyTelegram.ReadModel.Interfaces;
using MyTelegram.Messenger.Services;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.Services.Services;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class OfficialAccountCommandHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IMessageAppService messageAppService,
    IQueryProcessor queryProcessor,
    IRandomHelper randomHelper,
    ILogger<OfficialAccountCommandHandler> logger)
    : DomainEventHandlerBase(objectMessageSender, commandBus, idGenerator, ackCacheService),
        ISubscribeSynchronousTo<MessageAggregate, MessageId, InboxMessageCreatedEvent>
{
    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var item = domainEvent.AggregateEvent.InboxMessageItem;

        // Only handle messages sent TO the official account
        if (item.OwnerPeer.PeerId != MyTelegramConsts.OfficialUserId)
        {
            return;
        }

        // Check if sender is a support user
        var sender = await queryProcessor.ProcessAsync(new GetUserByIdQuery(item.SenderUserId), cancellationToken);
        if (sender == null || !sender.Support)
        {
            return; // Silently ignore non-support users
        }

        // Only process commands (messages starting with /)
        if (string.IsNullOrEmpty(item.Message) || !item.Message.StartsWith("/"))
        {
            return;
        }

        var parts = item.Message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var command = parts[0].ToLower();

        switch (command)
        {
            case "/lookup":
                await HandleLookupCommandAsync(item.SenderUserId, parts.Length > 1 ? parts[1] : null, item.MessageId, cancellationToken);
                break;
            case "/verify":
                await HandleStatusCommandAsync(item.SenderUserId, parts, "verify", item.MessageId, cancellationToken);
                break;
            case "/premium":
                await HandleStatusCommandAsync(item.SenderUserId, parts, "premium", item.MessageId, cancellationToken);
                break;
            case "/support":
                await HandleStatusCommandAsync(item.SenderUserId, parts, "support", item.MessageId, cancellationToken);
                break;
            case "/sessions":
                await HandleSessionsCommandAsync(item.SenderUserId, parts.Length > 1 ? parts[1] : null, item.MessageId, cancellationToken);
                break;
            case "/help":
                await SendReplyAsync(item.SenderUserId, GetHelpText(), item.MessageId);
                break;
            default:
                await SendReplyAsync(item.SenderUserId, "Unknown command. Type /help for available commands.", item.MessageId);
                break;
        }
    }

    private async Task HandleLookupCommandAsync(long adminId, string? query, int replyToMsgId, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(query))
        {
            await SendReplyAsync(adminId, "Usage: `/lookup @username`, `/lookup +phone`, or `/lookup #id`", replyToMsgId);
            return;
        }

        var (peerId, peerType) = await ResolvePeerAsync(query, ct);
        if (peerId == 0)
        {
            await SendReplyAsync(adminId, $"Could not resolve peer: `{query}`", replyToMsgId);
            return;
        }

        switch (peerType)
        {
            case PeerType.User:
                await DisplayUserDetailsAsync(adminId, peerId, replyToMsgId, ct);
                break;
            case PeerType.Channel:
                await DisplayChannelDetailsAsync(adminId, peerId, replyToMsgId, ct);
                break;
            case PeerType.Chat:
                await DisplayChatDetailsAsync(adminId, peerId, replyToMsgId, ct);
                break;
            default:
                await SendReplyAsync(adminId, $"Unsupported peer type: `{peerType}`", replyToMsgId);
                break;
        }
    }

    private async Task<(long peerId, PeerType peerType)> ResolvePeerAsync(string query, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(query)) return (0, PeerType.User);

        if (query.StartsWith("@"))
        {
            var username = query.TrimStart('@');
            var usernameData = await queryProcessor.ProcessAsync(new GetUserNameByNameQuery(username), ct);
            if (usernameData != null)
            {
                return (usernameData.PeerId, usernameData.PeerType);
            }
        }
        else if (query.StartsWith("+"))
        {
            var phone = query.TrimStart('+');
            var user = await queryProcessor.ProcessAsync(new GetUserByPhoneNumberQuery(phone), ct);
            if (user != null) return (user.UserId, PeerType.User);
        }
        else if (query.StartsWith("#"))
        {
            if (long.TryParse(query.TrimStart('#'), out var id))
            {
                return await ResolveIdAsync(id, ct);
            }
        }
        else if (long.TryParse(query, out var numericId))
        {
            // Try as ID first (User/Channel/Chat)
            var (peerId, peerType) = await ResolveIdAsync(numericId, ct);
            if (peerId != 0) return (peerId, peerType);

            // If not an ID, try as phone number
            var user = await queryProcessor.ProcessAsync(new GetUserByPhoneNumberQuery(query), ct);
            if (user != null) return (user.UserId, PeerType.User);
        }

        return (0, PeerType.User);
    }

    private async Task<(long peerId, PeerType peerType)> ResolveIdAsync(long id, CancellationToken ct)
    {
        var user = await queryProcessor.ProcessAsync(new GetUserByIdQuery(id), ct);
        if (user != null) return (user.UserId, PeerType.User);

        var channel = await queryProcessor.ProcessAsync(new GetChannelByIdQuery(id), ct);
        if (channel != null) return (channel.ChannelId, PeerType.Channel);

        var chat = await queryProcessor.ProcessAsync(new GetChatByChatIdQuery(id), ct);
        if (chat != null) return (id, PeerType.Chat);

        return (0, PeerType.User);
    }

    private async Task DisplayUserDetailsAsync(long adminId, long userId, int replyToMsgId, CancellationToken ct)
    {
        var user = await queryProcessor.ProcessAsync(new GetUserByIdQuery(userId), ct);
        if (user == null)
        {
            await SendReplyAsync(adminId, "User not found.", replyToMsgId);
            return;
        }

        var sessions = await queryProcessor.ProcessAsync(new GetDeviceByUserIdQuery(userId), ct);
        var sessionCount = sessions?.Count ?? 0;

        var botInfo = "";
        if (user.Bot)
        {
            var bot = await queryProcessor.ProcessAsync(new GetBotByIdQuery(userId), ct);
            if (bot != null)
            {
                botInfo = $"\nOwner ID: `{bot.OwnerUserId}`";
            }
        }

        var text = $"**User Details:**\n" +
                   $"ID: `{user.UserId}`\n" +
                   $"Phone: `{user.PhoneNumber}`\n" +
                   $"Name: {user.FirstName} {user.LastName}\n" +
                   $"Username: @{user.UserName ?? "none"}\n" +
                   $"Verified: {user.Verified}\n" +
                   $"Premium: {user.Premium}\n" +
                   $"Support: {user.Support}\n" +
                   $"Bot: {user.Bot}{botInfo}\n" +
                   $"Active Sessions: `{sessionCount}`";

        await SendReplyAsync(adminId, text, replyToMsgId);
    }

    private async Task DisplayChannelDetailsAsync(long adminId, long channelId, int replyToMsgId, CancellationToken ct)
    {
        var channel = await queryProcessor.ProcessAsync(new GetChannelByIdQuery(channelId), ct);
        if (channel == null)
        {
            await SendReplyAsync(adminId, "Channel not found.", replyToMsgId);
            return;
        }

        var type = channel.Broadcast ? "Channel" : "Supergroup";
        var admins = await queryProcessor.ProcessAsync(new GetChannelMembersByChannelIdQuery(channelId, [], 0, 100, OnlyAdmin: true), ct);
        
        // Format admin list with usernames
        var adminList = new List<string>();
        if (admins != null && admins.Count > 0)
        {
            var adminUserIds = admins.Select(a => a.UserId).ToList();
            var adminUsers = await queryProcessor.ProcessAsync(new GetUsersByUserIdListQuery(adminUserIds), ct);
            var userDict = adminUsers.ToDictionary(u => u.UserId);

            foreach (var admin in admins)
            {
                if (userDict.TryGetValue(admin.UserId, out var adminUser) && !string.IsNullOrEmpty(adminUser.UserName))
                {
                    adminList.Add($"@{adminUser.UserName}");
                }
                else
                {
                    adminList.Add($"`{admin.UserId}`");
                }
            }
        }

        var adminListText = adminList.Count > 0 ? string.Join(", ", adminList) : "none";

        var text = $"**{type} Details:**\n" +
                   $"ID: `{channel.ChannelId}`\n" +
                   $"Title: {channel.Title}\n" +
                   $"Username: @{channel.UserName ?? "none"}\n" +
                   $"Members: `{channel.ParticipantsCount ?? 0}`\n" +
                   $"Admins: {adminListText}";

        await SendReplyAsync(adminId, text, replyToMsgId);
    }

    private async Task DisplayChatDetailsAsync(long adminId, long chatId, int replyToMsgId, CancellationToken ct)
    {
        var chat = await queryProcessor.ProcessAsync(new GetChatByChatIdQuery(chatId), ct);
        if (chat == null)
        {
            await SendReplyAsync(adminId, "Chat not found.", replyToMsgId);
            return;
        }

        await SendReplyAsync(adminId, $"**Group Details:**\nID: `{chatId}`\n(Detailed group info not available via current read model)", replyToMsgId);
    }

    private async Task HandleSessionsCommandAsync(long adminId, string? query, int replyToMsgId, CancellationToken ct)
    {
        var (peerId, peerType) = await ResolvePeerAsync(query ?? "", ct);
        if (peerId == 0 || peerType != PeerType.User)
        {
            await SendReplyAsync(adminId, "Please specify a valid @user or #userid", replyToMsgId);
            return;
        }

        // Fetch user to get username for display
        var user = await queryProcessor.ProcessAsync(new GetUserByIdQuery(peerId), ct);
        var displayName = !string.IsNullOrEmpty(user?.UserName) ? $"@{user.UserName}" : peerId.ToString();

        var devices = await queryProcessor.ProcessAsync(new GetDeviceByUserIdQuery(peerId), ct);
        if (devices == null || devices.Count == 0)
        {
            await SendReplyAsync(adminId, $"No active sessions for {displayName}", replyToMsgId);
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine($"**Sessions for {displayName} ({devices.Count}):**\n");

        var sessionNumber = 1;
        foreach (var device in devices)
        {
            var activeDate = DateTimeOffset.FromUnixTimeSeconds(device.DateActive).ToString("MMM dd, HH:mm");
            
            // Improved Client & OS detection
            var ua = (device.DeviceModel ?? "" + " " + device.Platform ?? "").ToLower();
            var os = "Unknown OS";
            if (ua.Contains("android")) os = "Android";
            else if (ua.Contains("iphone") || ua.Contains("ipad") || ua.Contains("ios")) os = "iOS";
            else if (ua.Contains("mac") || ua.Contains("macintosh") || ua.Contains("macintel")) os = "macOS";
            else if (ua.Contains("windows")) os = "Windows";
            else if (ua.Contains("linux")) os = "Linux";

            var client = device.AppName;
            var dm = device.DeviceModel ?? "";
            if (dm.Contains("Chrome")) client = "Chrome";
            else if (dm.Contains("Safari") && !dm.Contains("Chrome") && !dm.Contains("Android")) client = "Safari";
            else if (dm.Contains("Firefox")) client = "Firefox";
            else if (dm.Contains("Edge")) client = "Edge";
            
            sb.AppendLine($"{sessionNumber}. **{os} ({client})** • `{device.Ip}` • {activeDate}");
            sessionNumber++;
        }

        await SendReplyAsync(adminId, sb.ToString(), replyToMsgId);
    }

    private async Task HandleStatusCommandAsync(long adminId, string[] parts, string statusType, int replyToMsgId, CancellationToken ct)
    {
        if (parts.Length < 2)
        {
            await SendReplyAsync(adminId, $"Usage: /{statusType} @username [on/off]", replyToMsgId);
            return;
        }

        var (peerId, peerType) = await ResolvePeerAsync(parts[1], ct);
        if (peerId == 0 || peerType != PeerType.User)
        {
            await SendReplyAsync(adminId, $"User not found: {parts[1]}", replyToMsgId);
            return;
        }

        var user = await queryProcessor.ProcessAsync(new GetUserByIdQuery(peerId), ct);
        if (user == null) return;

        bool enable;
        if (parts.Length >= 3)
        {
            enable = parts[2].ToLower() == "on";
        }
        else
        {
            enable = statusType switch
            {
                "verify" => !user.Verified,
                "premium" => !user.Premium,
                "support" => !user.Support,
                _ => false
            };
        }

        var aggregateId = UserId.Create(peerId);

        switch (statusType)
        {
            case "verify":
                await commandBus.PublishAsync(new SetVerifiedCommand(aggregateId, enable), ct);
                break;
            case "premium":
                await commandBus.PublishAsync(new UpdateUserPremiumStatusCommand(aggregateId, enable), ct);
                break;
            case "support":
                await commandBus.PublishAsync(new SetSupportCommand(aggregateId, enable), ct);
                break;
        }

        await SendReplyAsync(adminId, $"User @{user.UserName ?? user.UserId.ToString()} **{statusType}** status set to: **{enable}**", replyToMsgId);
    }

    private string GetHelpText()
    {
        return "**Support Admin Commands:**\n\n" +
               "**Lookup:**\n" +
               "/lookup @peer/#id/+num - Details for User, Bot, Channel, or Group\n" +
               "/sessions @user - List active sessions\n\n" +
               "**Status Management:**\n" +
               "/verify @user [on/off] - Toggle or set verified status\n" +
               "/premium @user [on/off] - Toggle or set premium status\n" +
               "/support @user [on/off] - Toggle or set support status\n\n" +
               "/help - Show this message";
    }

    private async Task SendReplyAsync(long toUserId, string text, int replyToMsgId)
    {
        var entities = ParseMarkdown(text, out var plainText);

        var sendMessageInput = new SendMessageInput(
            RequestInfo.Empty with
            {
                UserId = MyTelegramConsts.OfficialUserId,
                Date = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                RequestId = Guid.NewGuid(),
                DeviceType = DeviceType.Desktop
            },
            MyTelegramConsts.OfficialUserId,
            new Peer(PeerType.User, toUserId),
            plainText,
            randomHelper.NextInt64(),
            inputReplyTo: new TInputReplyToMessage { ReplyToMsgId = replyToMsgId },
            entities: new TVector<IMessageEntity>(entities));

        await messageAppService.SendMessageAsync([sendMessageInput]);
    }

    private List<IMessageEntity> ParseMarkdown(string markdown, out string plainText)
    {
        var entities = new List<IMessageEntity>();
        var sb = new StringBuilder();
        var current = 0;

        while (current < markdown.Length)
        {
            if (markdown.Substring(current).StartsWith("**"))
            {
                var end = markdown.IndexOf("**", current + 2);
                if (end != -1)
                {
                    var content = markdown.Substring(current + 2, end - (current + 2));
                    entities.Add(new TMessageEntityBold { Offset = sb.Length, Length = content.Length });
                    sb.Append(content);
                    current = end + 2;
                    continue;
                }
            }

            if (markdown.Substring(current).StartsWith("`"))
            {
                var end = markdown.IndexOf("`", current + 1);
                if (end != -1)
                {
                    var content = markdown.Substring(current + 1, end - (current + 1));
                    entities.Add(new TMessageEntityCode { Offset = sb.Length, Length = content.Length });
                    sb.Append(content);
                    current = end + 1;
                    continue;
                }
            }

            sb.Append(markdown[current]);
            current++;
        }

        plainText = sb.ToString();
        return entities;
    }
}

/*
TODO: Potential Additional Administrative Actions to Implement

## User Management
- Delete User Account - /delete @user - Permanently delete a user and their data
- Reset Password - /resetpw @user - Force password reset for a user
- Terminate Sessions - /logout @user - Force logout all sessions for a user (security)
- View Login History - /logins @user - Show recent login attempts and locations

## Content Moderation
- Delete Message - /delmsg @channel #msgid - Remove specific messages from channels
- View User Messages - /messages @user [limit] - View recent messages sent by a user
- Clear Chat - /clear @user @chat - Delete all messages between a user and a chat

## Channel/Group Management
- Ban from Channel - /chanban @user @channel - Ban user from specific channel
- Make Admin - /makeadmin @user @channel - Promote user to admin in a channel
- Remove Admin - /rmadmin @user @channel - Demote admin from a channel
- Transfer Ownership - /transfer @channel @newowner - Transfer channel ownership

## Statistics & Analytics
- Platform Stats - /stats - Show total users, active users, channels, etc.
- User Activity - /activity @user - Show user activity patterns
- Popular Channels - /popular [count] - List most active channels
- Growth Metrics - /growth [period] - Show platform growth over time

## Broadcast & Communication
- Broadcast Message - /broadcast [message] - Send announcement to all users
- Send Notice - /notice @user [message] - Send official notice to specific user
- Alert Admins - /alert [message] - Notify all admin users of an issue

## Data Management
- Export User Data - /export @user - Generate user data export (GDPR compliance)
- Backup Channel - /backup @channel - Create backup of channel messages
- Search Content - /search [query] - Search across all messages (with filters)

## Security & Compliance
- View Reports - /reports [@user] - Show abuse reports filed by/against users
- Block IP - /blockip [ip] - Block specific IP addresses
- Quarantine User - /quarantine @user - Temporary restriction while investigating
- Audit Log - /audit @user - View all admin actions performed on a user
*/
