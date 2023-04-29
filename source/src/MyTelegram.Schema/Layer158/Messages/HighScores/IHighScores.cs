// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IHighScores : IObject
{
    TVector<Schema.IHighScore> Scores { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
