namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Forwards messages by their IDs.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BROADCAST_PUBLIC_VOTERS_FORBIDDEN You can't forward polls with public voters.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 406 CHAT_FORWARDS_RESTRICTED You can't forward messages from a protected chat.
/// 403 CHAT_GUEST_SEND_FORBIDDEN You join the discussion group before commenting, see <a href="https://corefork.telegram.org/api/discussion#requiring-users-to-join-the-group">here »</a> for more info.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_RESTRICTED You can't send messages in this chat, you were restricted.
/// 403 CHAT_SEND_AUDIOS_FORBIDDEN You can't send audio messages in this chat.
/// 403 CHAT_SEND_DOCS_FORBIDDEN You can't send documents in this chat.
/// 403 CHAT_SEND_GAME_FORBIDDEN You can't send a game to this chat.
/// 403 CHAT_SEND_GIFS_FORBIDDEN You can't send gifs in this chat.
/// 403 CHAT_SEND_MEDIA_FORBIDDEN You can't send media in this chat.
/// 403 CHAT_SEND_PHOTOS_FORBIDDEN You can't send photos in this chat.
/// 403 CHAT_SEND_PLAIN_FORBIDDEN You can't send non-media (text) messages in this chat.
/// 403 CHAT_SEND_POLL_FORBIDDEN You can't send polls in this chat.
/// 403 CHAT_SEND_STICKERS_FORBIDDEN You can't send stickers in this chat.
/// 403 CHAT_SEND_VIDEOS_FORBIDDEN You can't send videos in this chat.
/// 403 CHAT_SEND_VOICES_FORBIDDEN You can't send voice recordings in this chat.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 GROUPED_MEDIA_INVALID Invalid grouped media.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MEDIA_EMPTY The provided media object is invalid.
/// 400 MESSAGE_IDS_EMPTY No message ids were provided.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 406 PAYMENT_UNSUPPORTED A detailed description of the error will be received separately as described <a href="https://corefork.telegram.org/api/errors#406-not-acceptable">here »</a>.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// 406 PRIVACY_PREMIUM_REQUIRED You need a <a href="https://corefork.telegram.org/api/premium">Telegram Premium subscription</a> to send a message to this user.
/// 400 QUICK_REPLIES_TOO_MUCH A maximum of <a href="https://corefork.telegram.org/api/config#quick-replies-limit">appConfig.<code>quick_replies_limit</code></a> shortcuts may be created, the limit was reached.
/// 400 QUIZ_ANSWER_MISSING You can forward a quiz while hiding the original author only after choosing an option in the quiz.
/// 500 RANDOM_ID_DUPLICATE You provided a random ID that was already used.
/// 400 RANDOM_ID_INVALID A provided random ID is invalid.
/// 400 REPLY_MESSAGES_TOO_MUCH Each shortcut can contain a maximum of <a href="https://corefork.telegram.org/api/config#quick-reply-messages-limit">appConfig.<code>quick_reply_messages_limit</code></a> messages, the limit was reached.
/// 400 SCHEDULE_BOT_NOT_ALLOWED Bots cannot schedule messages.
/// 400 SCHEDULE_DATE_TOO_LATE You can't schedule a message this far in the future.
/// 400 SCHEDULE_TOO_MUCH There are too many scheduled messages.
/// 400 SEND_AS_PEER_INVALID You can't send messages as the specified peer.
/// 400 SLOWMODE_MULTI_MSGS_DISABLED Slowmode is enabled, you cannot forward multiple messages to this group.
/// 420 SLOWMODE_WAIT_%d Slowmode is enabled in this chat: wait %d seconds before sending another message to this chat.
/// 406 TOPIC_CLOSED This topic was closed, you can't send messages to it anymore.
/// 406 TOPIC_DELETED The specified topic was deleted.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// 403 USER_IS_BLOCKED You were blocked by this user.
/// 400 USER_IS_BOT Bots can't send messages to other bots.
/// 403 VOICE_MESSAGES_FORBIDDEN This user's privacy settings forbid you from sending voice messages.
/// 400 YOU_BLOCKED_USER You blocked this user.
/// See <a href="https://corefork.telegram.org/method/messages.forwardMessages" />
///</summary>
internal sealed class ForwardMessagesHandler(
    ICommandBus commandBus,
    IPeerHelper peerHelper,
    IChannelAppService channelAppService,
    IMessageAppService messageAppService,
    IQueryProcessor queryProcessor,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestForwardMessages, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestForwardMessages obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.FromPeer);
        await accessHashHelper.CheckAccessHashAsync(input, obj.ToPeer);
        await accessHashHelper.CheckAccessHashAsync(input, obj.SendAs);

        var fromPeer = peerHelper.GetPeer(obj.FromPeer, input.UserId);
        var toPeer = peerHelper.GetPeer(obj.ToPeer, input.UserId);
        var sendAs = peerHelper.GetPeer(obj.SendAs);

        messageAppService.CheckBotPermission(input.UserId, toPeer);

        var post = false;
        var fromNames = new Dictionary<long, string>();

        if (toPeer.PeerType == PeerType.Channel)
        {
            await messageAppService.CheckSendAsAsync(input.UserId, toPeer, sendAs);

            var channelReadModel = await channelAppService.GetAsync(toPeer.PeerId);
            post = channelReadModel!.Broadcast;

            if (channelReadModel.Broadcast || channelReadModel.LinkedChatId != null && sendAs == null)
            {
                if (await messageAppService.CanSendAsPeerAsync(channelReadModel.ChannelId, input.UserId))
                {
                    var defaultSendAs = await queryProcessor.ProcessAsync(
                        new GetUserConfigByKeyQuery(input.UserId, ((int)UserConfigType.SendAsPeer).ToString()));
                    if (defaultSendAs != null)
                    {
                        if (long.TryParse(defaultSendAs.Value, out var sendAsPeerId))
                        {
                            var tempSendAs = peerHelper.GetPeer(sendAsPeerId);
                            if (await messageAppService.IsValidSendAsPeerAsync(input.UserId, toPeer, tempSendAs))
                            {
                                sendAs = tempSendAs;
                            }
                        }
                    }

                    sendAs ??= toPeer;
                }
            }

            if (channelReadModel.CreatorId != input.UserId)
            {
                var admin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == input.UserId);

                if (post)
                {
                    if (admin == null || !admin.AdminRights.PostMessages)
                    {
                        RpcErrors.RpcErrors403.ChatWriteForbidden.ThrowRpcError();
                    }
                }
                else
                {
                    var bannedDefaultRights =
                        channelReadModel.DefaultBannedRights ?? ChatBannedRights.CreateDefaultBannedRights();
                    if (bannedDefaultRights.SendMessages)
                    {
                        RpcErrors.RpcErrors403.ChatWriteForbidden.ThrowRpcError();
                    }
                }
            }
        }

        var command = new StartForwardMessagesCommand(TempId.New,
            input.ToRequestInfo(),
            obj.Silent,
            obj.Background,
            obj.WithMyScore,
            obj.DropAuthor,
            obj.DropMediaCaptions,
            obj.Noforwards,
            fromPeer,
            toPeer,
            obj.Id.ToList(),
            obj.RandomId.ToList(),
            obj.ScheduleDate,
            sendAs,
            false,
            post,
            null,
            fromNames
            );
        await commandBus.PublishAsync(command);

        return null!;
    }
}
