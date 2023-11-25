// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Saved contact
/// See <a href="https://corefork.telegram.org/constructor/SavedContact" />
///</summary>
[JsonDerivedType(typeof(TSavedPhoneContact), nameof(TSavedPhoneContact))]
public interface ISavedContact : IObject
{
    ///<summary>
    /// Phone number
    ///</summary>
    string Phone { get; set; }

    ///<summary>
    /// First name
    ///</summary>
    string FirstName { get; set; }

    ///<summary>
    /// Last name
    ///</summary>
    string LastName { get; set; }

    ///<summary>
    /// Date added
    ///</summary>
    int Date { get; set; }
}
