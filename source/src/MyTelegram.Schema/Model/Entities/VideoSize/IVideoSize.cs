// ReSharper disable All

namespace MyTelegram.Schema;

public interface IVideoSize : IObject
{
    BitArray Flags { get; set; }
    string Type { get; set; }
    int W { get; set; }
    int H { get; set; }
    int Size { get; set; }
    double? VideoStartTs { get; set; }

}
