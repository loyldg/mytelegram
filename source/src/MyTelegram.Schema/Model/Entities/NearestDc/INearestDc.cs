// ReSharper disable All

namespace MyTelegram.Schema;

public interface INearestDc : IObject
{
    string Country { get; set; }
    int ThisDc { get; set; }
    int NearestDc { get; set; }

}
