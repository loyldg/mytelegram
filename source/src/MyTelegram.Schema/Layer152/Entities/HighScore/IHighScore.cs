// ReSharper disable All

namespace MyTelegram.Schema;

public interface IHighScore : IObject
{
    int Pos { get; set; }
    long UserId { get; set; }
    int Score { get; set; }
}
