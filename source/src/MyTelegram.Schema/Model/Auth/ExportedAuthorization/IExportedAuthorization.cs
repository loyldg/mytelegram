// ReSharper disable All

namespace MyTelegram.Schema.Auth;

public interface IExportedAuthorization : IObject
{
    long Id { get; set; }
    byte[] Bytes { get; set; }

}
