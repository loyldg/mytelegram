using EventFlow.Core;

namespace MyTelegram.ReadModel.ReadModelLocators;

public class ChatInviteImporterId : Identity<ChatInviteImporterId>
{
    public ChatInviteImporterId(string value) : base(value)
    {
    }

    public static ChatInviteImporterId Create(long peerId, long userId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"chatinviteimporter-{peerId}-{userId}");
    }
}