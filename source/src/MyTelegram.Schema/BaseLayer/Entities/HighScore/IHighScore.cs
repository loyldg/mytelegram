// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Game high score
/// See <a href="https://corefork.telegram.org/constructor/HighScore" />
///</summary>
[JsonDerivedType(typeof(THighScore), nameof(THighScore))]
public interface IHighScore : IObject
{
    ///<summary>
    /// Position in highscore list
    ///</summary>
    int Pos { get; set; }

    ///<summary>
    /// User ID
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// Score
    ///</summary>
    int Score { get; set; }
}
