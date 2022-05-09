using MyTelegram.Schema.Channels;
using IChatFull = MyTelegram.Schema.Messages.IChatFull;

namespace MyTelegram.MessengerServer.DomainEventHandlers;

public interface ITlChatConverter
{
    IChat ToChat(long chatId,
        string title,
        int date,
        int memberCount);
    IChat ToChannel(IChannelReadModel channelReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        long selfUserId, bool channelMemberIsLeft);
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

    Schema.IChannelParticipant ToChannelParticipant(IChannelReadModel channelReadModel,
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
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        long selfUserId);

    IChatFull ToChatFull(IChannelReadModel channelReadModel,
        IChannelFullReadModel channelFullReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        long selfUserId);
}