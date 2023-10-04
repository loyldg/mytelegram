// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/Booster" />
///</summary>
public interface IBooster : IObject
{
    ///<summary>
    /// &nbsp;
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int Expires { get; set; }
}
