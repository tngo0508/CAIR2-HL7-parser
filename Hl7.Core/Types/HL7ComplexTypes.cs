namespace Hl7.Core.Types;

/// <summary>
/// CE - Coded Element
/// </summary>
public class CE : CompositeDataType
{
    /// <summary>
    /// Gets the identifier
    /// </summary>
    public string Identifier => GetComponent(0);

    /// <summary>
    /// Gets the text
    /// </summary>
    public string Text => GetComponent(1);

    /// <summary>
    /// Gets the name of the coding system
    /// </summary>
    public string NameOfCodingSystem => GetComponent(2);

    /// <summary>
    /// Gets the alternate identifier
    /// </summary>
    public string AlternateIdentifier => GetComponent(3);

    /// <summary>
    /// Gets the alternate text
    /// </summary>
    public string AlternateText => GetComponent(4);

    /// <summary>
    /// Gets the name of the alternate coding system
    /// </summary>
    public string NameOfAlternateCodingSystem => GetComponent(5);

    /// <summary>
    /// Initializes a new instance of the <see cref="CE"/> class
    /// </summary>
    public CE() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CE"/> class with a value
    /// </summary>
    /// <param name="value">The coded element value</param>
    public CE(string value) : base(value) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CE"/> class with a value and separators
    /// </summary>
    public CE(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    /// <summary>
    /// Implicit conversion from string to CE
    /// </summary>
    /// <param name="value">The string value</param>
    public static implicit operator CE(string value) => new CE(value);

    /// <summary>
    /// Gets the descriptive name of a component at the specified index
    /// </summary>
    public override string GetComponentName(int index) => index switch
    {
        0 => "Identifier",
        1 => "Text",
        2 => "Name of Coding System",
        3 => "Alternate Identifier",
        4 => "Alternate Text",
        5 => "Name of Alternate Coding System",
        _ => base.GetComponentName(index)
    };
}

/// <summary>
/// CX - Extended Composite ID with Check Digit
/// </summary>
public class CX : CompositeDataType
{
    /// <summary>
    /// Gets the ID number
    /// </summary>
    public string IDNumber => GetComponent(0);

    /// <summary>
    /// Gets the check digit
    /// </summary>
    public string CheckDigit => GetComponent(1);

    /// <summary>
    /// Gets the check digit scheme
    /// </summary>
    public string CheckDigitScheme => GetComponent(2);

    /// <summary>
    /// Gets the assigning authority
    /// </summary>
    public string AssigningAuthority => GetComponent(3);

    /// <summary>
    /// Gets the identifier type code
    /// </summary>
    public string IdentifierTypeCode => GetComponent(4);

    /// <summary>
    /// Gets the assigning facility
    /// </summary>
    public string AssigningFacility => GetComponent(5);

    /// <summary>
    /// Initializes a new instance of the <see cref="CX"/> class
    /// </summary>
    public CX() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CX"/> class with a value
    /// </summary>
    /// <param name="value">The composite ID value</param>
    public CX(string value) : base(value) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CX"/> class with a value and separators
    /// </summary>
    public CX(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    /// <summary>
    /// Implicit conversion from string to CX
    /// </summary>
    /// <param name="value">The string value</param>
    public static implicit operator CX(string value) => new CX(value);

    /// <summary>
    /// Gets the descriptive name of a component at the specified index
    /// </summary>
    public override string GetComponentName(int index) => index switch
    {
        0 => "ID Number",
        1 => "Check Digit",
        2 => "Check Digit Scheme",
        3 => "Assigning Authority",
        4 => "Identifier Type Code",
        5 => "Assigning Facility",
        6 => "Effective Date",
        7 => "Expiration Date",
        _ => base.GetComponentName(index)
    };
}

/// <summary>
/// XPN - Extended Person Name
/// </summary>
public class XPN : CompositeDataType
{
    /// <summary>
    /// Gets the family name
    /// </summary>
    public string FamilyName => GetComponent(0);

    /// <summary>
    /// Gets the given name
    /// </summary>
    public string GivenName => GetComponent(1);

    /// <summary>
    /// Gets the second and further given names or initials
    /// </summary>
    public string SecondAndFurtherGivenNamesOrInitials => GetComponent(2);

    /// <summary>
    /// Gets the suffix
    /// </summary>
    public string Suffix => GetComponent(3);

    /// <summary>
    /// Gets the prefix
    /// </summary>
    public string Prefix => GetComponent(4);

    /// <summary>
    /// Gets the degree
    /// </summary>
    public string Degree => GetComponent(5);

    /// <summary>
    /// Gets the name type code
    /// </summary>
    public string NameTypeCode => GetComponent(6);

    /// <summary>
    /// Initializes a new instance of the <see cref="XPN"/> class
    /// </summary>
    public XPN() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="XPN"/> class with a value
    /// </summary>
    /// <param name="value">The person name value</param>
    public XPN(string value) : base(value) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="XPN"/> class with a value and separators
    /// </summary>
    public XPN(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    /// <summary>
    /// Implicit conversion from string to XPN
    /// </summary>
    /// <param name="value">The string value</param>
    public static implicit operator XPN(string value) => new XPN(value);

    /// <summary>
    /// Gets the descriptive name of a component at the specified index
    /// </summary>
    public override string GetComponentName(int index) => index switch
    {
        0 => "Family Name",
        1 => "Given Name",
        2 => "Second and Further Given Names or Initials",
        3 => "Suffix",
        4 => "Prefix",
        5 => "Degree",
        6 => "Name Type Code",
        7 => "Name Representation Code",
        _ => base.GetComponentName(index)
    };
}

/// <summary>
/// XAD - Extended Address
/// </summary>
public class XAD : CompositeDataType
{
    /// <summary>
    /// Gets the street address
    /// </summary>
    public string StreetAddress => GetComponent(0);

    /// <summary>
    /// Gets the other designation
    /// </summary>
    public string OtherDesignation => GetComponent(1);

    /// <summary>
    /// Gets the city
    /// </summary>
    public string City => GetComponent(2);

    /// <summary>
    /// Gets the state or province
    /// </summary>
    public string StateOrProvince => GetComponent(3);

    /// <summary>
    /// Gets the zip or postal code
    /// </summary>
    public string ZipOrPostalCode => GetComponent(4);

    /// <summary>
    /// Gets the country
    /// </summary>
    public string Country => GetComponent(5);

    /// <summary>
    /// Gets the address type
    /// </summary>
    public string AddressType => GetComponent(6);

    /// <summary>
    /// Gets the other geographic designation
    /// </summary>
    public string OtherGeographicDesignation => GetComponent(7);

    /// <summary>
    /// Gets the county/parish code
    /// </summary>
    public string CountyParishCode => GetComponent(8);

    /// <summary>
    /// Initializes a new instance of the <see cref="XAD"/> class
    /// </summary>
    public XAD() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="XAD"/> class with a value
    /// </summary>
    /// <param name="value">The address value</param>
    public XAD(string value) : base(value) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="XAD"/> class with a value and separators
    /// </summary>
    public XAD(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    /// <summary>
    /// Implicit conversion from string to XAD
    /// </summary>
    /// <param name="value">The string value</param>
    public static implicit operator XAD(string value) => new XAD(value);

    /// <summary>
    /// Gets the descriptive name of a component at the specified index
    /// </summary>
    public override string GetComponentName(int index) => index switch
    {
        0 => "Street Address",
        1 => "Other Designation",
        2 => "City",
        3 => "State or Province",
        4 => "Zip or Postal Code",
        5 => "Country",
        6 => "Address Type",
        7 => "Other Geographic Designation",
        8 => "County/Parish Code",
        9 => "Census Tract",
        10 => "Address Representation Code",
        11 => "Address Validity Range",
        _ => base.GetComponentName(index)
    };
}

/// <summary>
/// XTN - Extended Telecommunication Number
/// </summary>
public class XTN : CompositeDataType
{
    /// <summary>
    /// Gets the telephone number
    /// </summary>
    public string TelephoneNumber => GetComponent(0);

    /// <summary>
    /// Gets the telecommunication use code
    /// </summary>
    public string TelecommunicationUseCode => GetComponent(1);

    /// <summary>
    /// Gets the telecommunication equipment type
    /// </summary>
    public string TelecommunicationEquipmentType => GetComponent(2);

    /// <summary>
    /// Gets the email address
    /// </summary>
    public string EmailAddress => GetComponent(3);

    /// <summary>
    /// Gets the country code
    /// </summary>
    public string CountryCode => GetComponent(4);

    /// <summary>
    /// Gets the area/city code
    /// </summary>
    public string AreaCityCode => GetComponent(5);

    /// <summary>
    /// Gets the local number
    /// </summary>
    public string LocalNumber => GetComponent(6);

    /// <summary>
    /// Gets the extension
    /// </summary>
    public string Extension => GetComponent(7);

    /// <summary>
    /// Initializes a new instance of the <see cref="XTN"/> class
    /// </summary>
    public XTN() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="XTN"/> class with a value
    /// </summary>
    /// <param name="value">The telecommunication number value</param>
    public XTN(string value) : base(value) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="XTN"/> class with a value and separators
    /// </summary>
    public XTN(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    /// <summary>
    /// Implicit conversion from string to XTN
    /// </summary>
    /// <param name="value">The string value</param>
    public static implicit operator XTN(string value) => new XTN(value);

    /// <summary>
    /// Gets the descriptive name of a component at the specified index
    /// </summary>
    public override string GetComponentName(int index) => index switch
    {
        0 => "Telephone Number",
        1 => "Telecommunication Use Code",
        2 => "Telecommunication Equipment Type",
        3 => "Email Address",
        4 => "Country Code",
        5 => "Area/City Code",
        6 => "Local Number",
        7 => "Extension",
        8 => "Any Text",
        _ => base.GetComponentName(index)
    };
}
