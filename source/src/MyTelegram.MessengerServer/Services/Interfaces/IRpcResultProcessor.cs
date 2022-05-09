using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Contacts;
using MyTelegram.Schema.Messages;
using MyTelegram.Schema.Phone;
using MyTelegram.Schema.Updates;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;
using IChannelParticipant = MyTelegram.Schema.Channels.IChannelParticipant;
using IChatFull = MyTelegram.Schema.Messages.IChatFull;
using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;
using IPhoto = MyTelegram.Schema.Photos.IPhoto;

namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IRpcResultProcessor
{
    IAuthorization CreateAuthorization(SignInSuccessEvent aggregateEvent);
    IAuthorization CreateAuthorizationFromUser(IUserReadModel? user);
    IAuthorization CreateAuthorizationFromUser(UserCreatedEvent userCreatedEvent);
    IAuthorization CreateSignUpAuthorization();
    Schema.IAuthorization ToAuthorization(IDeviceReadModel deviceReadModel);


    IReadOnlyList<Schema.IAuthorization> ToAuthorizations(IReadOnlyCollection<IDeviceReadModel> deviceList,
        long selfPermAuthKeyId);

    IChat ToChannel(IChannelReadModel channelReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        long selfUserId);

    IChannelDifference ToChannelDifference(GetMessageOutput output,
        bool isChannelMember,
        IList<IUpdate> updates,
        int updatesMaxPts = 0,
        bool resetLeftToFalse = false);

    List<IChat> ToChannelList(
        IReadOnlyCollection<IChannelReadModel> channelReadModels,
        IReadOnlyCollection<long> joinedChannelIdList,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        long selfUserId,
        bool resetLeftToFalse = false);

    IChannelParticipant ToChannelParticipant(IChannelReadModel channelReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        IUserReadModel userReadModel,
        long selfUserId);

    Schema.LayerN.IChannelParticipant ToChannelParticipantLayerN(IChannelReadModel channelReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        IUserReadModel userReadModel,
        long selfUserId);

    IChannelParticipants ToChannelParticipants(IChannelReadModel channelReadModel,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        IReadOnlyCollection<IUserReadModel> userReadModels,
        long selfUserId,
        DeviceType deviceType,
        bool forceNotLeft);

    IChat ToChat(IChatReadModel chat,
        long selfUserId);

    IChatFull ToChatFull(IChatReadModel chat,
        IReadOnlyCollection<IUserReadModel> userList,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel,
        long selfUserId);

    IChatFull ToChatFull(IChannelReadModel channelReadModel,
        IChannelFullReadModel channelFullReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel,
        long selfUserId);
    IUpdates ToDeleteMessagesUpdates(PeerType toPeerType,
        DeletedBoxItem item,
        int date);

    IDialogs ToDialogs(GetDialogOutput output);

    IDifference ToDifference(GetMessageOutput output,
        IPtsReadModel? pts,
        int cachedPts,
        int limit,
        IList<IUpdate> updateList,
        IList<IChat> chatListFromUpdates);

    IExportedChatInvite ToExportedChatInvite(ExportChatInviteEvent eventData);
    IFound ToFound(SearchContactOutput output);
    IJoinAsPeers ToJoinAsPeers(IUserReadModel userReadModel,
        IChannelReadModel? channelReadModel,
        IChatReadModel? chatReadModel);
    IMessages ToMessages(GetMessageOutput output);

    IPeerDialogs ToPeerDialogs(GetDialogOutput output);
    IPhoto ToPhoto(UserProfilePhotoChangedEvent aggregateEvent);

    IUpdates ToReadHistoryUpdates(ReadHistoryCompletedEvent eventData);
    IUser ToUser(UserNameUpdatedEvent aggregateEvent);

    IUser ToUser(IUserReadModel user,
        long selfUserId);

    Task<IUserFull> ToUserFullAsync(IUserReadModel user,
        long selfUserId,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel
    );
}