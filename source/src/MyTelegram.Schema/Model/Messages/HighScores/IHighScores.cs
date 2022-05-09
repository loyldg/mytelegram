// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IHighScores : IObject
{
    TVector<MyTelegram.Schema.IHighScore> Scores { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
