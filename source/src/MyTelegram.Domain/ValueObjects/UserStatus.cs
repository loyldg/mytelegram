using System.Text;

namespace MyTelegram.Domain.ValueObjects;

public class UserStatus
{
    public UserStatus(long userId,
        bool online)
    {
        UserId = userId;
        Online = online;
        LastUpdateDate = DateTime.UtcNow;
    }

    public DateTime LastUpdateDate { get; internal set; }
    public bool Online { get; internal set; }

    public long UserId { get; internal set; }

    public void UpdateStatus(bool online)
    {
        Online = online;
        LastUpdateDate = DateTime.UtcNow;
    }
}

public class Reaction : ValueObject
{
    //public Reaction()
    //{
    //}

    public Reaction(long userId, string? emoticon,
        long? customEmojiDocumentId, int? date = 0)
    {
        UserId = userId;
        Emoticon = emoticon;
        CustomEmojiDocumentId = customEmojiDocumentId;
        Date = date;
    }

    public long UserId { get; set; }
    public string? Emoticon { get; set; }
    public long? CustomEmojiDocumentId { get; set; }

    public int? Date { get; set; }
    //public int? ChosenOrder { get; set; }

    //public int GetReactionCount() => _count;

    public long GetReactionId()
    {
        if (CustomEmojiDocumentId.HasValue)
        {
            return CustomEmojiDocumentId.Value;
        }

        if (string.IsNullOrEmpty(Emoticon))
        {
            throw new InvalidOperationException("Emotion and CustomEmojiDocumentId is null");
        }
        var bytes = Encoding.UTF8.GetBytes(Emoticon);
        if (bytes.Length >= 8)
        {
            return BitConverter.ToInt64(bytes);
        }

        var newBytes = new byte[8];
        Buffer.BlockCopy(bytes, 0, newBytes, 0, bytes.Length);

        return BitConverter.ToInt64(newBytes);
    }
}

public class ReactionCount : ValueObject
{
    public string? Emoticon { get; internal set; }
    public long? CustomEmojiDocumentId { get; internal set; }
    public int Count { get; internal set; }
    public int? ChosenOrder { get; set; }
    //public bool? CanSeeList { get; internal set; }

    //public ReactionCount()
    //{
    //}

    public ReactionCount(string? emoticon,
        long? customEmojiDocumentId, int count)
    {
        Emoticon = emoticon;
        CustomEmojiDocumentId = customEmojiDocumentId;
        Count = count;
    }
    public void IncrementCount()
    {
        Count++;
    }

    public void DecrementCount()
    {
        Count--;
    }

    public long GetReactionId()
    {
        if (CustomEmojiDocumentId.HasValue)
        {
            return CustomEmojiDocumentId.Value;
        }

        if (string.IsNullOrEmpty(Emoticon))
        {
            throw new InvalidOperationException("Emotion and CustomEmojiDocumentId is null");
        }
        var bytes = Encoding.UTF8.GetBytes(Emoticon);
        if (bytes.Length >= 8)
        {
            return BitConverter.ToInt64(bytes);
        }

        var newBytes = new byte[8];
        Buffer.BlockCopy(bytes, 0, newBytes, 0, bytes.Length);

        return BitConverter.ToInt64(newBytes);
    }
}