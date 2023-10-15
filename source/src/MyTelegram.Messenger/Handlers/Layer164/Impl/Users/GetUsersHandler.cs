// ReSharper disable All

namespace MyTelegram.Handlers.Users;

///<summary>
/// Returns basic user info according to their identifiers.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 FROM_MESSAGE_BOT_DISABLED Bots can't use fromMessage min constructors.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/users.getUsers" />
///</summary>
internal sealed class GetUsersHandler : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetUsers, TVector<MyTelegram.Schema.IUser>>,
    Users.IGetUsersHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IUserConverter> _layeredService;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPhotoAppService _photoAppService;
    private readonly IPrivacyAppService _privacyAppService;

    public GetUsersHandler(IQueryProcessor queryProcessor,
        ILayeredService<IUserConverter> layeredService,
        IAccessHashHelper accessHashHelper, IPhotoAppService photoAppService, IPrivacyAppService privacyAppService)
    {
        _queryProcessor = queryProcessor;
        _layeredService = layeredService;
        _accessHashHelper = accessHashHelper;
        _photoAppService = photoAppService;
        _privacyAppService = privacyAppService;
    }

    protected override async Task<TVector<IUser>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Users.RequestGetUsers obj)
    {
        var userIds = new List<long>();
        foreach (var inputUser in obj.Id)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputUser);
            switch (inputUser)
            {
                case TInputUser inputUser1:
                    userIds.Add(inputUser1.UserId);
                    break;
                case TInputUserEmpty inputUserEmpty:
                    userIds.Add(input.UserId);
                    break;
                case TInputUserFromMessage inputUserFromMessage:
                    userIds.Add(inputUserFromMessage.UserId);
                    break;
                case TInputUserSelf inputUserSelf:
                    userIds.Add(input.UserId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(inputUser));
            }
        }

        var users = await _queryProcessor.ProcessAsync(new GetUsersByUidListQuery(userIds), default)
     ;

        var contacts = await _queryProcessor.ProcessAsync(new GetContactListQuery(input.UserId, userIds), default)
     ;

        var privacies = await _privacyAppService.GetPrivacyListAsync(userIds);

        var photos = await _photoAppService.GetPhotosAsync(users, contacts);

        var r = _layeredService.GetConverter(input.Layer).ToUserList(input.UserId, users, photos, contacts, privacies);

        return new TVector<IUser>(r);
    }
}
