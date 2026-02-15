namespace Hl7.Core.Types;

/// <summary>
/// CE - Coded Element
/// </summary>
public class CE : CompositeDataType
{
    public string Identifier => GetComponent(0);
    public string Text => GetComponent(1);
    public string NameOfCodingSystem => GetComponent(2);
    public string AlternateIdentifier => GetComponent(3);
    public string AlternateText => GetComponent(4);
    public string NameOfAlternateCodingSystem => GetComponent(5);

    public CE() { }
    public CE(string value) : base(value) { }
    public static implicit operator CE(string value) => new CE(value);
}

/// <summary>
/// CX - Extended Composite ID with Check Digit
/// </summary>
public class CX : CompositeDataType
{
    public string IDNumber => GetComponent(0);
    public string CheckDigit => GetComponent(1);
    public string CheckDigitScheme => GetComponent(2);
    public string AssigningAuthority => GetComponent(3);
    public string IdentifierTypeCode => GetComponent(4);
    public string AssigningFacility => GetComponent(5);

    public CX() { }
    public CX(string value) : base(value) { }
    public static implicit operator CX(string value) => new CX(value);
}

/// <summary>
/// XPN - Extended Person Name
/// </summary>
public class XPN : CompositeDataType
{
    public string FamilyName => GetComponent(0);
    public string GivenName => GetComponent(1);
    public string SecondAndFurtherGivenNamesOrInitials => GetComponent(2);
    public string Suffix => GetComponent(3);
    public string Prefix => GetComponent(4);
    public string Degree => GetComponent(5);
    public string NameTypeCode => GetComponent(6);

    public XPN() { }
    public XPN(string value) : base(value) { }
    public static implicit operator XPN(string value) => new XPN(value);
}

/// <summary>
/// XAD - Extended Address
/// </summary>
public class XAD : CompositeDataType
{
    public string StreetAddress => GetComponent(0);
    public string OtherDesignation => GetComponent(1);
    public string City => GetComponent(2);
    public string StateOrProvince => GetComponent(3);
    public string ZipOrPostalCode => GetComponent(4);
    public string Country => GetComponent(5);
    public string AddressType => GetComponent(6);
    public string OtherGeographicDesignation => GetComponent(7);
    public string CountyParishCode => GetComponent(8);

    public XAD() { }
    public XAD(string value) : base(value) { }
    public static implicit operator XAD(string value) => new XAD(value);
}

/// <summary>
/// XTN - Extended Telecommunication Number
/// </summary>
public class XTN : CompositeDataType
{
    public string TelephoneNumber => GetComponent(0);
    public string TelecommunicationUseCode => GetComponent(1);
    public string TelecommunicationEquipmentType => GetComponent(2);
    public string EmailAddress => GetComponent(3);
    public string CountryCode => GetComponent(4);
    public string AreaCityCode => GetComponent(5);
    public string LocalNumber => GetComponent(6);
    public string Extension => GetComponent(7);

    public XTN() { }
    public XTN(string value) : base(value) { }
    public static implicit operator XTN(string value) => new XTN(value);
}
