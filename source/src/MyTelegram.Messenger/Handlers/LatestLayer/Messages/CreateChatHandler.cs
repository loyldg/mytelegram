namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Creates a new chat.
/// <para>Possible errors</para>
/// Code Type Description
/// 500 CHAT_ID_GENERATE_FAILED Failure while generating the chat ID.
/// 400 CHAT_INVALID Invalid chat.
/// 400 CHAT_TITLE_EMPTY No chat title provided.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 TTL_PERIOD_INVALID The specified TTL period is invalid.
/// 400 USERS_TOO_FEW Not enough users (to create a chat, for example).
/// 406 USER_RESTRICTED You're spamreported, you can't create channels or chats.
/// See <a href="https://corefork.telegram.org/method/messages.createChat" />
///</summary>
internal sealed class CreateChatHandler(
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IRandomHelper randomHelper,
    IAccessHashHelper accessHashHelper,
    IQueryProcessor queryProcessor,
    IPeerHelper peerHelper,
    IPrivacyAppService privacyAppService)
    : RpcResultObjectHandler<Schema.Messages.RequestCreateChat, Schema.Messages.IInvitedUsers>
{
    protected override async Task<Schema.Messages.IInvitedUsers> HandleCoreAsync(IRequestInput input,
        RequestCreateChat obj)
    {
        var ttl = obj.TtlPeriod;
        var ttlFromDefaultSetting = false;
        if (ttl == null || ttl == 0)
        {
            ttlFromDefaultSetting = ttl != null && ttl != 0;
        }

        var memberUserIds = new List<long>();
        var botUserIds = new List<long>();
        foreach (var inputUser in obj.Users)
        {
            if (inputUser is TInputUser u)
            {
                await accessHashHelper.CheckAccessHashAsync(input, u.UserId, u.AccessHash, AccessHashType.User);
                memberUserIds.Add(u.UserId);

                if (peerHelper.IsBotUser(u.UserId))
                {
                    botUserIds.Add(u.UserId);
                }
            }
        }

        memberUserIds = memberUserIds.Distinct().ToList();

        var channelId = await idGenerator.NextLongIdAsync(IdType.ChannelId);
        var accessHash = randomHelper.NextInt64();
        var date = DateTime.UtcNow.ToTimestamp();

        var privacyRestrictedUserIdList = new List<long>();
        await privacyAppService.ApplyPrivacyListAsync(input.UserId, memberUserIds,
          (_, restrictedUserId) => privacyRestrictedUserIdList.Add(restrictedUserId), new List<PrivacyType>
            {
                    PrivacyType.ChatInvite
            });
        memberUserIds.RemoveAll(privacyRestrictedUserIdList.Contains);

        if (botUserIds.Count > 0)
        {
            var botReadModels = await queryProcessor.ProcessAsync(new GetBotListQuery(botUserIds));
            var needRemoveUserIdsFromList = new List<long>();
            foreach (var botReadModel in botReadModels)
            {
                if (!botReadModel.AllowJoinGroups)
                {
                    needRemoveUserIdsFromList.Add(botReadModel.UserId);
                }
            }

            botUserIds.RemoveAll(needRemoveUserIdsFromList.Contains);
            memberUserIds.RemoveAll(needRemoveUserIdsFromList.Contains);
        }

        var createChannelCommand = new CreateChannelCommand(ChannelId.Create(channelId),
            input.ToRequestInfo(),
            channelId,
            input.UserId,
            obj.Title,
            //obj.Broadcast,
            false,
            true,
            string.Empty,
            null,
            null,
            accessHash,
            date,
            randomHelper.NextInt64(),
            new TMessageActionChannelCreate { Title = obj.Title },
            ttl,
            false,
            null,
            null,
            null,
            true,
            ttlFromDefaultSetting,
            memberUserIds,
            botUserIds
        );
        await commandBus.PublishAsync(createChannelCommand);


        return null!;
    }
}