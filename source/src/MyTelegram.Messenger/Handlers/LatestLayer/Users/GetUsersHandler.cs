namespace MyTelegram.Messenger.Handlers.LatestLayer.Users;
/// <summary>
/// Returns basic user info according to their identifiers.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_MONOFORUM_UNSUPPORTED <a href="https://corefork.telegram.org/api/channel#monoforums">Monoforums</a> do not support this feature.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 FROM_MESSAGE_BOT_DISABLED Bots can't use fromMessage min constructors.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// <para><c>See <a href="https://corefork.telegram.org/method/users.getUsers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class GetUsersHandler(IAccessHashHelper accessHashHelper, IUserConverterService userConverterService) : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetUsers, TVector<MyTelegram.Schema.IUser>>
{
    protected override async Task<TVector<IUser>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Users.RequestGetUsers obj)
    {
        var userIds = new List<long>();
        var result = new TVector<IUser>();
        foreach (var inputUser in obj.Id)
        {
            switch (inputUser)
            {
                case TInputUser inputUser1:
                    if (await accessHashHelper.IsAccessHashValidAsync(input, inputUser1.UserId, inputUser1.AccessHash))
                    {
                        userIds.Add(inputUser1.UserId);
                    }

                    result.Add(new TUserEmpty { Id = inputUser1.UserId });
                    break;
                case TInputUserEmpty:
                    userIds.Add(input.UserId);
                    result.Add(new TUserEmpty { Id = input.UserId });
                    break;
                case TInputUserFromMessage:
                    result.Add(new TUserEmpty { Id = 0 });
                    //userIds.Add(inputUserFromMessage.UserId);
                    break;
                case TInputUserSelf:
                    userIds.Add(input.UserId);
                    result.Add(new TUserEmpty { Id = input.UserId });
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        var users = (await userConverterService.GetUserListAsync(input, userIds, false, false, input.Layer)).ToDictionary(k => k.Id);
        for (int i = 0; i < result.Count; i++)
        {
            if (users.TryGetValue(result[i].Id, out var user))
            {
                result[i] = user;
            }
        }

        return result;
    }
}