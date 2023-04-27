// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAuthorization : IObject
{
    BitArray Flags { get; set; }
    bool Current { get; set; }
    bool OfficialApp { get; set; }
    bool PasswordPending { get; set; }
    bool EncryptedRequestsDisabled { get; set; }
    bool CallRequestsDisabled { get; set; }
    long Hash { get; set; }
    string DeviceModel { get; set; }
    string Platform { get; set; }
    string SystemVersion { get; set; }
    int ApiId { get; set; }
    string AppName { get; set; }
    string AppVersion { get; set; }
    int DateCreated { get; set; }
    int DateActive { get; set; }
    string Ip { get; set; }
    string Country { get; set; }
    string Region { get; set; }
}
