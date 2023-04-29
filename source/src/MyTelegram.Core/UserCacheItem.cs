namespace MyTelegram.Core;

public class UserCacheItem
{
    //public string PhoneNumber { get; set; }
    public long UserId { get; set; }

    public static string GetCacheKey(string phoneNumber)
    {
        return $"user_{phoneNumber.ToPhoneNumber()}";
    }
}
