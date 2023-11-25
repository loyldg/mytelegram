// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Popular contact
/// See <a href="https://corefork.telegram.org/constructor/PopularContact" />
///</summary>
[JsonDerivedType(typeof(TPopularContact), nameof(TPopularContact))]
public interface IPopularContact : IObject
{
    ///<summary>
    /// Contact identifier
    ///</summary>
    long ClientId { get; set; }

    ///<summary>
    /// How many people imported this contact
    ///</summary>
    int Importers { get; set; }
}
