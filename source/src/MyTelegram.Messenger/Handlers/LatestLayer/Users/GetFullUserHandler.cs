using MyTelegram.Messenger.Services.Impl;
using TUserFull = MyTelegram.Schema.Users.TUserFull;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Users;

///<summary>
/// Returns extended user info by ID.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USERNAME_OCCUPIED The provided username is already occupied.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/users.getFullUser" />
///</summary>
internal sealed class GetFullUserHandler(
    IPeerHelper peerHelper,
    IQueryProcessor queryProcessor,
    IUserConverterService userConverterService,
    ILayeredService<IPeerSettingsConverter> peerSettingsLayeredService,
    ILayeredService<IPeerNotifySettingsConverter> peerNotifySettingsLayeredService,
    IBlockCacheAppService blockCacheAppService,
    IAccessHashHelper accessHashHelper,
    IContactHelper contactHelper,
    IPeerSettingsAppService peerSettingsAppService,
    IPhotoAppService photoAppService,
    IUserAppService userAppService,
    IPrivacyAppService privacyAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetFullUser,
            MyTelegram.Schema.Users.IUserFull>
{
    protected override async Task<MyTelegram.Schema.Users.IUserFull> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Users.RequestGetFullUser obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Id);

        var selfUserId = input.UserId;
        var targetPeer = peerHelper.GetPeer(obj.Id, input.UserId);
        var targetUserId = targetPeer.PeerId;

        var userReadModel = await userAppService.GetAsync(targetPeer.PeerId);
        if (userReadModel == null)
        {
            //RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
            throw new RpcException(RpcErrors.RpcErrors400.UserIdInvalid);
        }

        var photoReadModels = await photoAppService.GetPhotosAsync(userReadModel);
        var privacyReadModels = await privacyAppService.GetPrivacyListAsync(targetUserId);
        var contactReadModels =
            await queryProcessor.ProcessAsync(new GetContactListBySelfIdAndTargetUserIdQuery(input.UserId, targetUserId));

        var myContactReadModel =
              contactReadModels?.FirstOrDefault(p => p.SelfUserId == selfUserId && p.TargetUserId == targetUserId);
        var targetUserContactReadModel =
              contactReadModels?.FirstOrDefault(p => p.SelfUserId == targetUserId && p.TargetUserId == selfUserId);

        var peerNotifySettingsId = PeerNotifySettingsId.Create(selfUserId, targetPeer.PeerType, targetPeer.PeerId);
        var peerNotifySettingReadModel =
            await queryProcessor.ProcessAsync(new GetPeerNotifySettingsByIdQuery(peerNotifySettingsId.Value));
        var peerSettingReadModel = await peerSettingsAppService.GetPeerSettingsAsync(input.UserId, targetPeer.PeerId);
        var contactType = contactHelper.GetContactType(myContactReadModel, targetUserContactReadModel);// await contactAppService.GetContactTypeAsync(input.UserId, targetPeer.PeerId);
        var peerSettings = peerSettingsLayeredService.GetConverter(input.Layer).ToPeerSettings(input.UserId,
            targetPeer.PeerId,
            peerSettingReadModel,
            contactType
        );
        var peerNotifySettings = peerNotifySettingsLayeredService.GetConverter(input.Layer)
            .ToPeerNotifySettings(peerNotifySettingReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings);

        var userFull = userConverterService.ToUserFull(input, userReadModel, photoReadModels, contactReadModels,
            privacyReadModels, input.Layer);

        userFull.Settings = peerSettings;
        userFull.NotifySettings = peerNotifySettings;
        userFull.Blocked = await blockCacheAppService.IsBlockedAsync(input.UserId, targetPeer.PeerId);

        var user = userConverterService.ToUser(input, userReadModel, photoReadModels, myContactReadModel,
            targetUserContactReadModel, privacyReadModels, input.Layer);

        await SetPersonalChannelAsync(input, userReadModel, userFull);
        await SetCommonChatCountAsync(input, userReadModel, userFull);

        return new TUserFull
        {
            Chats = [],
            FullUser = userFull,
            Users = new TVector<IUser>(user)
        };
    }

    private async Task SetCommonChatCountAsync(IRequestInput input, IUserReadModel userReadModel, IUserFull userFull)
    {
        var count = await queryProcessor.ProcessAsync(new GetCommonChatCountQuery(input.UserId, userReadModel.UserId));
        userFull.CommonChatsCount = count;
    }

    private async Task SetPersonalChannelAsync(IRequestInput input, IUserReadModel userReadModel, IUserFull userFull)
    {
        if (userReadModel.PersonalChannelId.HasValue)
        {
            var channelTopMessageId =
                await queryProcessor.ProcessAsync(
                    new GetChannelTopMessageIdQuery(userReadModel.PersonalChannelId.Value));

            if (channelTopMessageId.HasValue)
            {
                userFull.PersonalChannelId = userReadModel.PersonalChannelId;
                userFull.PersonalChannelMessage = channelTopMessageId.Value;
            }
        }
    }
}
