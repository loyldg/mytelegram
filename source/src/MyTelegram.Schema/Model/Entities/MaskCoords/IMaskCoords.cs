// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMaskCoords : IObject
{
    int N { get; set; }
    double X { get; set; }
    double Y { get; set; }
    double Zoom { get; set; }

}
