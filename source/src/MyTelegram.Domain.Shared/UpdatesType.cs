namespace MyTelegram;

public enum UpdatesType
{
    Unknown,
    /// <summary>
    /// TUpdates
    /// </summary>
    Updates,

    /// <summary>
    /// Other IUpdates(TUpdateShort,TUpdateShortChatMessage,TUpdateShortMessage,TUpdateShortSentMessage)
    /// </summary>
    NewMessages,
    NewEncryptedMessages,
    EncryptedUpdates
}