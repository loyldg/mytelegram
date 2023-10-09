using EventFlow.Core;

namespace MyTelegram.ReadModel.ReadModelLocators;

public class ChatInviteId : Identity<ChatInviteId>
{
    public ChatInviteId(string value) : base(value)
    {
    }

    public static ChatInviteId Create(long peerId, long inviteId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"chatinviteid-{peerId}-{inviteId}");
    }
}