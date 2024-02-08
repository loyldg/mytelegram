// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Creates a new chat.May also return 0-N updates of type <a href="https://corefork.telegram.org/constructor/updateGroupInvitePrivacyForbidden">updateGroupInvitePrivacyForbidden</a>: it indicates we couldn't add a user to a chat because of their privacy settings; if required, an <a href="https://corefork.telegram.org/api/invites">invite link</a> can be shared with the user, instead.
/// <para>Possible errors</para>
/// Code Type Description
/// 500 CHAT_ID_GENERATE_FAILED Failure while generating the chat ID.
/// 400 CHAT_INVALID Invalid chat.
/// 400 CHAT_TITLE_EMPTY No chat title provided.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 USERS_TOO_FEW Not enough users (to create a chat, for example).
/// 406 USER_RESTRICTED You're spamreported, you can't create channels or chats.
/// See <a href="https://corefork.telegram.org/method/messages.createChat" />
///</summary>
internal sealed class CreateChatHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestCreateChat, MyTelegram.Schema.IUpdates>,
    Messages.ICreateChatHandler
{
    private readonly IIdGenerator _idGenerator;
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPrivacyAppService _privacyAppService;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;

    public CreateChatHandler(ICommandBus commandBus,
        IIdGenerator idGenerator,
        IRandomHelper randomHelper,
        IAccessHashHelper accessHashHelper, IPeerHelper peerHelper, IPrivacyAppService privacyAppService, IOptions<MyTelegramMessengerServerOptions> options)
    {
        _commandBus = commandBus;
        _idGenerator = idGenerator;
        _randomHelper = randomHelper;
        _accessHashHelper = accessHashHelper;
        _peerHelper = peerHelper;
        _privacyAppService = privacyAppService;
        _options = options;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestCreateChat obj)
    {
        var memberUidList = new List<long>();
        foreach (var inputUser in obj.Users)
        {
            if (inputUser is TInputUser u)
            {
                await _accessHashHelper.CheckAccessHashAsync(u.UserId, u.AccessHash);
                memberUidList.Add(u.UserId);
            }
        }

        memberUidList = memberUidList.Distinct().ToList();

        if (_options.Value.AutoCreateSuperGroup)
        {
            var channelId = await _idGenerator.NextLongIdAsync(IdType.ChannelId);
            var accessHash = _randomHelper.NextLong();
            var date = DateTime.UtcNow.ToTimestamp();
            var createChannelCommand = new CreateChannelCommand(ChannelId.Create(channelId),
                input.ToRequestInfo(),
                channelId,
                input.UserId,
                obj.Title,
                //obj.Broadcast,
                false,
                true,
                string.Empty,
                string.Empty,
                accessHash,
                date,
                _randomHelper.NextLong(),
                new TMessageActionChannelCreate { Title = obj.Title }.ToBytes().ToHexString(),
                null,
                false,
                null,
                null,
                null
            );
            await _commandBus.PublishAsync(createChannelCommand, CancellationToken.None);

            var userIdList = new List<long>();
            var botList = new List<long>();
            foreach (TInputUser inputUser in obj.Users)
            {
                userIdList.Add(inputUser.UserId);
                if (_peerHelper.IsBotUser(inputUser.UserId))
                {
                    botList.Add(inputUser.UserId);
                }
            }

            var privacyRestrictedUserIdList = new List<long>();
            await _privacyAppService.ApplyPrivacyListAsync(input.UserId, userIdList,
                privacyRestrictedUserIdList.Add, new List<PrivacyType>
                {
                    PrivacyType.ChatInvite
                });
            userIdList.RemoveAll(privacyRestrictedUserIdList.Contains);

            // all selected users are rejected to be added to chat or channel
            if (userIdList.Count == 0)
            {

            }

            var command = new StartInviteToChannelCommand(
                ChannelId.Create(channelId),
                input.ToRequestInfo(),
                channelId,
                input.UserId,
                1,//default maxMessageId 1
                userIdList,
                privacyRestrictedUserIdList,
                botList,
                CurrentDate,
                _randomHelper.NextLong(),
                new TMessageActionChatAddUser { Users = new TVector<long>(userIdList) }.ToBytes().ToHexString(),
                ChatJoinType.InvitedByAdmin
                );
            await _commandBus.PublishAsync(command, CancellationToken.None);

        }
        else
        {
            var chatId = await _idGenerator.NextLongIdAsync(IdType.ChatId);
            var randomId = _randomHelper.NextLong();
            var messageActionData =
                new TMessageActionChatCreate { Title = obj.Title, Users = new TVector<long>(memberUidList) }.ToBytes()
                    .ToHexString();

            var command = new CreateChatCommand(ChatId.Create(chatId),
                input.ToRequestInfo(),
                chatId,
                input.UserId,
                obj.Title,
                memberUidList,
                CurrentDate,
                randomId,
                messageActionData,
                obj.TtlPeriod
                );
            await _commandBus.PublishAsync(command, CancellationToken.None);
        }

        return null!;
    }
}
