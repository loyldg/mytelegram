// ReSharper disable once CheckNamespace
namespace MyTelegram;

public enum MessageSubType
{
    None = 0,
    Normal = 1,
    CreateChat = 2,
    CreateChannel = 3,
    InviteToChannel = 4,
    UpdatePinnedMessage = 5,
    ForwardMessage = 6,
    AddChatUser = 7,
    DeleteChatUser = 8,
    ClearHistory = 9,
    LeaveChat = 10,
    ForwardMessageToLinkedGroup = 11,
    PhoneCall = 12,
    CreateGroupCall = 13,
    CreateChannelForumTopic = 14,
    EditForumTopic = 15,
    MigrateChat = 16,
}