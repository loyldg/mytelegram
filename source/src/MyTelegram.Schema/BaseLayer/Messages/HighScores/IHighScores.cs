// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// High scores (in games)
/// See <a href="https://corefork.telegram.org/constructor/messages.HighScores" />
///</summary>
public interface IHighScores : IObject
{
    ///<summary>
    /// Highscores
    /// See <a href="https://corefork.telegram.org/type/HighScore" />
    ///</summary>
    TVector<MyTelegram.Schema.IHighScore> Scores { get; set; }

    ///<summary>
    /// Users, associated to the highscores
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
