// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IGroupCallStreamRtmpUrl : IObject
{
    string Url { get; set; }
    string Key { get; set; }

}
