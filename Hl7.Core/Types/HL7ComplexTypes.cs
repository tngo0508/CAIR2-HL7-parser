namespace Hl7.Core.Types;

/// <summary>
/// EI - Entity Identifier
/// </summary>
public class EI : CompositeDataType
{
    /// <summary>
    /// Gets the entity identifier
    /// </summary>
    public string EntityIdentifier => GetComponent(0);

    /// <summary>
    /// Gets the namespace ID
    /// </summary>
    public string NamespaceId => GetComponent(1);

    /// <summary>
    /// Gets the universal ID
    /// </summary>
    public string UniversalId => GetComponent(2);

    /// <summary>
    /// Gets the universal ID type
    /// </summary>
    public string UniversalIdType => GetComponent(3);

    public EI() { }
    public EI(string value) : base(value) { }
    public EI(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    public static implicit operator EI(string value) => new EI(value);

    public override string GetComponentName(int index) => index switch
    {
        0 => "Entity Identifier",
        1 => "Namespace ID",
        2 => "Universal ID",
        3 => "Universal ID Type",
        _ => base.GetComponentName(index)
    };
}

/// <summary>
/// HD - Hierarchic Designator
/// </summary>
public class HD : CompositeDataType
{
    /// <summary>
    /// Gets the namespace ID
    /// </summary>
    public string NamespaceId => GetComponent(0);

    /// <summary>
    /// Gets the universal ID
    /// </summary>
    public string UniversalId => GetComponent(1);

    /// <summary>
    /// Gets the universal ID type
    /// </summary>
    public string UniversalIdType => GetComponent(2);

    public HD() { }
    public HD(string value) : base(value) { }
    public HD(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    public static implicit operator HD(string value) => new HD(value);

    public override string GetComponentName(int index) => index switch
    {
        0 => "Namespace ID",
        1 => "Universal ID",
        2 => "Universal ID Type",
        _ => base.GetComponentName(index)
    };
}

/// <summary>
/// SAD - Street Address
/// </summary>
public class SAD : CompositeDataType
{
    /// <summary>
    /// Gets the street or mailing address
    /// </summary>
    public string StreetOrMailingAddress => GetComponent(0);

    /// <summary>
    /// Gets the street name
    /// </summary>
    public string StreetName => GetComponent(1);

    /// <summary>
    /// Gets the dwelling number
    /// </summary>
    public string DwellingNumber => GetComponent(2);

    public SAD() { }
    public SAD(string value) : base(value) { }
    public SAD(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    public static implicit operator SAD(string value) => new SAD(value);

    public override string GetComponentName(int index) => index switch
    {
        0 => "Street or Mailing Address",
        1 => "Street Name",
        2 => "Dwelling Number",
        _ => base.GetComponentName(index)
    };
}

/// <summary>
/// TSU - Time Stamp
/// </summary>
public class TSU : CompositeDataType
{
    /// <summary>
    /// Gets the time
    /// </summary>
    public string Time => GetComponent(0);

    /// <summary>
    /// Gets the degree of precision
    /// </summary>
    public string DegreeOfPrecision => GetComponent(1);

    public TSU() { }
    public TSU(string value) : base(value) { }
    public TSU(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    public static implicit operator TSU(string value) => new TSU(value);

    public override string GetComponentName(int index) => index switch
    {
        0 => "Time",
        1 => "Degree of Precision",
        _ => base.GetComponentName(index)
    };
}

/// <summary>
/// LA1 - Location with Address Variation 1
/// </summary>
public class LA1 : CompositeDataType
{
    public string PointOfCare => GetComponent(0);
    public string Room => GetComponent(1);
    public string Bed => GetComponent(2);
    public HD Facility => new HD(GetComponent(3));
    public string LocationStatus => GetComponent(4);
    public string PersonLocationType => GetComponent(5);
    public string Building => GetComponent(6);
    public string Floor => GetComponent(7);
    public string AddressText => GetComponent(8);

    public LA1() { }
    public LA1(string value) : base(value) { }
    public LA1(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    public static implicit operator LA1(string value) => new LA1(value);

    public override string GetComponentName(int index) => index switch
    {
        0 => "Point of Care",
        1 => "Room",
        2 => "Bed",
        3 => "Facility",
        4 => "Location Status",
        5 => "Person Location Type",
        6 => "Building",
        7 => "Floor",
        8 => "Address Text",
        _ => base.GetComponentName(index)
    };
}

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
/// VFC - VFC Eligibility (specialized CE)
/// </summary>
public class VFC : CE
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VFC"/> class
    /// </summary>
    public VFC() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="VFC"/> class with a value
    /// </summary>
    public VFC(string value) : base(value) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="VFC"/> class with a value and separators
    /// </summary>
    public VFC(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    /// <summary>
    /// Gets the descriptive name of a component at the specified index
    /// </summary>
    public override string GetComponentName(int index) => index switch
    {
        0 => "Identifier",
        1 => "Text",
        2 => "Name of Coding System",
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
        8 => "Assigning Jurisdiction",
        9 => "Assigning Organization",
        _ => base.GetComponentName(index)
    };
}

/// <summary>
/// ERN - Error (CDC v1.5)
/// </summary>
public class ERN : CompositeDataType
{
    public string SegmentId => GetComponent(0);
    public string Sequence => GetComponent(1);
    public string FieldPosition => GetComponent(2);
    public string FieldRepetition => GetComponent(3);
    public string ComponentNumber => GetComponent(4);
    public string SubComponentNumber => GetComponent(5);

    public ERN() { }
    public ERN(string value) : base(value) { }
    public ERN(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    public static implicit operator ERN(string value) => new ERN(value);

    public override string GetComponentName(int index) => index switch
    {
        0 => "Segment ID",
        1 => "Sequence",
        2 => "Field Position",
        3 => "Field Repetition",
        4 => "Component Number",
        5 => "Sub-Component Number",
        _ => base.GetComponentName(index)
    };
}

/// <summary>
/// DLN - Driver's License Number
/// </summary>
public class DLN : CompositeDataType
{
    public string LicenseNumber => GetComponent(0);
    public string IssuingState => GetComponent(1);
    public string ExpirationDate => GetComponent(2);

    public DLN() { }
    public DLN(string value) : base(value) { }
    public DLN(string value, char componentSeparator, char subComponentSeparator) 
        : base(value, componentSeparator, subComponentSeparator) { }

    public static implicit operator DLN(string value) => new DLN(value);

    public override string GetComponentName(int index) => index switch
    {
        0 => "License Number",
        1 => "Issuing State",
        2 => "Expiration Date",
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

    /// <summary>
    /// Gets the street address (specialized SAD)
    /// </summary>
    public SAD Street => new SAD(GetComponent(0));
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
        9 => "Extension Prefix",
        10 => "Speed Dial Code",
        11 => "Unformatted Telephone Number",
        _ => base.GetComponentName(index)
    };
}
