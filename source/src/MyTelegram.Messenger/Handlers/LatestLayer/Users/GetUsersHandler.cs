namespace MyTelegram.Messenger.Handlers.LatestLayer.Users;

///<summary>
/// Returns basic user info according to their identifiers.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 FROM_MESSAGE_BOT_DISABLED Bots can't use fromMessage min constructors.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/users.getUsers" />
///</summary>
internal sealed class GetUsersHandler(
    IUserConverterService userConverterService)
    : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetUsers, TVector<MyTelegram.Schema.IUser>>
{
    protected override async Task<TVector<IUser>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Users.RequestGetUsers obj)
    {
        var userIds = new List<long>();
        foreach (var inputUser in obj.Id)
        {
            //await _accessHashHelper.CheckAccessHashAsync(inputUser);
            switch (inputUser)
            {
                case TInputUser inputUser1:
                    userIds.Add(inputUser1.UserId);
                    break;
                case TInputUserEmpty:
                    userIds.Add(input.UserId);
                    break;
                case TInputUserFromMessage inputUserFromMessage:
                    userIds.Add(inputUserFromMessage.UserId);
                    break;
                case TInputUserSelf:
                    userIds.Add(input.UserId);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        var users = await userConverterService.GetUserListAsync(input, userIds, false, false, input.Layer);

        return [.. users];
    }
}
