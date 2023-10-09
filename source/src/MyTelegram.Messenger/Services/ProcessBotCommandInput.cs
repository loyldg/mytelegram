namespace MyTelegram.Messenger.Services;

public class ProcessBotCommandInput
{
    public ProcessBotCommandInput(long botUserId,
        long senderPeerId,
        int senderMessageId,
        int messageId,
        string command,
        //PeerType replyToPeerType,
        //long replyToPeerId,
        Peer replyToPeer,
        int updateId,
        bool isChannelPost = false,
        string? data = null,
        byte[]? media = null
    )
    {
        BotUserId = botUserId;
        SenderMessageId = senderMessageId;
        SenderPeerId = senderPeerId;
        MessageId = messageId;
        Command = command;
        ReplyToPeer = replyToPeer;
        //ReplyToPeerType = replyToPeerType;
        //ReplyToPeerId = replyToPeerId;
        UpdateId = updateId;
        IsChannelPost = isChannelPost;
        Data = data;
        Media = media;
    }

    public long BotUserId { get; }
    public int SenderMessageId { get; }
    public string Command { get; }
    public Peer ReplyToPeer { get; }
    public bool IsChannelPost { get; }
    public string? Data { get; }
    public byte[]? Media { get; }

    public int MessageId { get; }
    //public long ReplyToPeerId { get; }
    //public PeerType ReplyToPeerType { get; }
    public long SenderPeerId { get; }
    public int UpdateId { get; set; }
}