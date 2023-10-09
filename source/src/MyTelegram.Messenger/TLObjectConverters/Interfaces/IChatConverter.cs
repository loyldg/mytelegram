using IChannelParticipant = MyTelegram.Schema.Channels.IChannelParticipant;
using IChatFull = MyTelegram.Schema.IChatFull;
using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;

namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IChatConverter : ILayeredConverter, IHasRequestLayer
{
    IChat ToChannel(
        long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        //IChatPhoto chatPhoto,
        bool channelMemberIsLeft);

    ILayeredChannel ToChannel(ChannelCreatedEvent channelCreatedEvent);

    //TChannel ToChannel(long selfUserId,
    //    long channelId,
    //    string title,
    //    long creatorUid,
    //    bool broadcast,
    //    bool megaGroup,
    //    long accessHash,
    //    int date,
    //    int participantsCount);

    IChatFull ToChannelFull(
        long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelFullReadModel channelFullReadModel,
        //IPhoto chatFullPhoto,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel,
        IChatInviteReadModel? chatInviteReadModel = null

    );

    IList<IChat> ToChannelList(
        long selfUserId,
        IReadOnlyCollection<IChannelReadModel> channelReadModels,
        IReadOnlyCollection<IPhotoReadModel>? photoReadModels,
        IReadOnlyCollection<long> joinedChannelIdList,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        //IPhotoConverter photoConverter,
        bool resetLeftToFalse = false);

    IChannelParticipant ToChannelParticipant(
        long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        //IChatPhoto chatPhoto,
        //IUserReadModel userReadModel,
        IUser user
    //,
    //IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    );

    Schema.Channels.IChannelParticipant ToChannelParticipantLayerN(
        long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        //IChatPhoto chatPhoto,
        //IUserReadModel userReadModel,
        IUser user
    //,
    //IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    );

    IChannelParticipants ToChannelParticipants(
        long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IReadOnlyCollection<IChatAdminReadModel>? chatAdminReadModels,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        //IChatPhoto chatPhoto,
        //IReadOnlyCollection<IUserReadModel> userReadModels,
        IEnumerable<IUser> users,
        DeviceType deviceType,
        bool forceNotLeft
    //,
    //IReadOnlyCollection<IContactReadModel>? contactReadModels = null,
    //IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    );

    //IChat ToChat(long chatId,
    //    string title,
    //    int date,
    //    int memberCount);
    IChat ToChat(ChatCreatedEvent chatCreatedEvent);

    IChat ToChat(
        long selfUserId,
        IChatReadModel chat,
        IPhotoReadModel? photoReadModel
        //IChatPhoto chatPhoto,
        );

    Schema.Messages.IChatFull ToChatFull(
        long selfUserId,
        IChatReadModel chat,
        IPhotoReadModel? photoReadModel,
        //IChatPhoto chatPhoto,
        //IReadOnlyCollection<IUserReadModel> userList,
        IEnumerable<IUser> users,
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        IChannelReadModel? migratedToChannelReadModel = null,
        IChatInviteReadModel? chatInviteReadModel=null
    //,
    //IReadOnlyCollection<IContactReadModel>? contactReadModels = null,
    //IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    );

    Schema.Messages.IChatFull ToChatFull(
        long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelFullReadModel channelFullReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        //IChatPhoto chatPhoto,
        //IPhoto chatFullPhoto,
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        IChatReadModel? migratedFromChatReadModel,
        IChatInviteReadModel? chatInviteReadModel = null
        );

    IList<IChat> ToChatList(
        long selfUserId,
        IReadOnlyCollection<IChatReadModel> chats,
        IReadOnlyCollection<IPhotoReadModel>? photoReadModels);
    //IPhotoConverter photoConverter);

    //IExportedChatInvite ToExportedChatInvite(ChannelInviteExportedEvent eventData);

    IUpdates ToInviteToChannelUpdates(IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        //IChatPhoto chatPhoto,
        IUserReadModel senderUserReadModel,
        int date);
}
