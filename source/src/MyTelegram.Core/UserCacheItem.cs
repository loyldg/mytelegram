namespace MyTelegram.Core;

public class UserCacheItem
{
    //public string PhoneNumber { get; set; }
    public long UserId { get; set; }

    public static string GetCacheKey(string phoneNumber)
    {
        return MyCacheKey.With("user", "phone", phoneNumber.ToPhoneNumber());
        //$"users:user_{phoneNumber.ToPhoneNumber()}";
    }
}