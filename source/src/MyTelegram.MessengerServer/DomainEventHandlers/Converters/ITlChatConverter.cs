using MyTelegram.Schema.Channels;
using IChannelParticipant = MyTelegram.Schema.IChannelParticipant;
using IChatFull = MyTelegram.Schema.Messages.IChatFull;
using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlChatConverter
{
    IChat ToChannel(IChannelReadModel channelReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        long selfUserId,
        bool channelMemberIsLeft);

    TChannel ToChannel(long selfUserId,
        long channelId,
        string title,
        long creatorUid,
        bool broadcast,
        bool megaGroup,
        long accessHash,
        int date,
        int participantsCount);

    TChannelFull ToChannelFull(IChannelReadModel channelReadModel,
        IChannelFullReadModel channelFullReadModel,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel,
        long selfUserId
    );

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
    IChat ToChat(long chatId,
        string title,
        int date,
        int memberCount);

    IChat ToChat(IChatReadModel chat,
        long selfUserId);

    IChatFull ToChatFull(IChatReadModel chat,
        IReadOnlyCollection<IUserReadModel> userList,
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        long selfUserId);

    IChatFull ToChatFull(IChannelReadModel channelReadModel,
        IChannelFullReadModel channelFullReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        long selfUserId);

    IExportedChatInvite ToExportedChatInvite(ExportChatInviteEvent eventData);

    IUpdates ToInviteToChannelUpdates(IChannelReadModel channelReadModel,
        IUserReadModel senderUserReadModel,
        int date);
}
