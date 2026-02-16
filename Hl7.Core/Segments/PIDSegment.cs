using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;
using Hl7.Core.Types;

namespace Hl7.Core.Segments;

/// <summary>
/// PID - Patient Identification Segment
/// </summary>
[Segment("PID")]
public class PIDSegment : Segment
{
    /// <summary>
    /// Gets or sets the Set ID - PID (PID-1)
    /// </summary>
    [DataElement(1, "Set ID - PID", ElementUsage.Optional)]
    public int SetId { get; set; }

    /// <summary>
    /// Gets or sets the Patient ID (PID-2)
    /// </summary>
    [DataElement(2, "Patient ID", ElementUsage.Optional)]
    public string PatientId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Patient Identifier List (PID-3)
    /// </summary>
    [DataElement(3, "Patient Identifier List", ElementUsage.Optional)]
    public CX PatientIdentifierList { get; set; } = new();

    /// <summary>
    /// Gets or sets the Alternate Patient ID - PID (PID-4)
    /// </summary>
    [DataElement(4, "Alternate Patient ID - PID", ElementUsage.Optional)]
    public CX AlternatePatientId { get; set; } = new();

    /// <summary>
    /// Gets or sets the Patient Name (PID-5)
    /// </summary>
    [DataElement(5, "Patient Name", ElementUsage.Optional)]
    public XPN PatientName { get; set; } = new();

    /// <summary>
    /// Gets or sets the Mother's Maiden Name (PID-6)
    /// </summary>
    [DataElement(6, "Mother's Maiden Name", ElementUsage.Optional)]
    public XPN MothersMaidenName { get; set; } = new();

    /// <summary>
    /// Gets or sets the Date/Time of Birth (PID-7)
    /// </summary>
    [DataElement(7, "Date/Time of Birth", ElementUsage.Optional)]
    public string DateOfBirth { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Administrative Sex (PID-8)
    /// </summary>
    [DataElement(8, "Administrative Sex", ElementUsage.Optional)]
    public string AdministrativeSex { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Patient Alias (PID-9)
    /// </summary>
    [DataElement(9, "Patient Alias", ElementUsage.Optional)]
    public XPN PatientAlias { get; set; } = new();

    /// <summary>
    /// Gets or sets the Race (PID-10)
    /// </summary>
    [DataElement(10, "Race", ElementUsage.Optional)]
    public CE Race { get; set; } = new();

    /// <summary>
    /// Gets or sets the Patient Address (PID-11)
    /// </summary>
    [DataElement(11, "Patient Address", ElementUsage.Optional)]
    public XAD PatientAddress { get; set; } = new();

    /// <summary>
    /// Gets or sets the County Code (PID-12)
    /// </summary>
    [DataElement(12, "County Code", ElementUsage.Optional)]
    public string CountyCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Phone Number - Home (PID-13)
    /// </summary>
    [DataElement(13, "Phone Number - Home", ElementUsage.Optional)]
    public XTN PhoneNumberHome { get; set; } = new();

    /// <summary>
    /// Gets or sets the Phone Number - Business (PID-14)
    /// </summary>
    [DataElement(14, "Phone Number - Business", ElementUsage.Optional)]
    public XTN PhoneNumberBusiness { get; set; } = new();

    /// <summary>
    /// Gets or sets the Primary Language (PID-15)
    /// </summary>
    [DataElement(15, "Primary Language", ElementUsage.Optional)]
    public CE PrimaryLanguage { get; set; } = new();

    /// <summary>
    /// Gets or sets the Marital Status (PID-16)
    /// </summary>
    [DataElement(16, "Marital Status", ElementUsage.Optional)]
    public string MaritalStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Religion (PID-17)
    /// </summary>
    [DataElement(17, "Religion", ElementUsage.Optional)]
    public string Religion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Patient Account Number (PID-18)
    /// </summary>
    [DataElement(18, "Patient Account Number", ElementUsage.Optional)]
    public string PatientAccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the SSN Number - Patient (PID-19)
    /// </summary>
    [DataElement(19, "SSN Number - Patient", ElementUsage.Optional)]
    public string SSNNumberPatient { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Ethnic Group (PID-22)
    /// </summary>
    [DataElement(22, "Ethnic Group", ElementUsage.Optional)]
    public CE EthnicGroup { get; set; } = new();

    /// <summary>
    /// Gets or sets the Birth Place (PID-23)
    /// </summary>
    [DataElement(23, "Birth Place", ElementUsage.Optional)]
    public string BirthPlace { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Multiple Birth Indicator (PID-24)
    /// </summary>
    [DataElement(24, "Multiple Birth Indicator", ElementUsage.Optional)]
    public string MultipleBirthIndicator { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Birth Order (PID-25)
    /// </summary>
    [DataElement(25, "Birth Order", ElementUsage.Optional)]
    public string BirthOrder { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Citizenship (PID-26)
    /// </summary>
    [DataElement(26, "Citizenship", ElementUsage.Optional)]
    public string Citizenship { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Veterans Military Status (PID-27)
    /// </summary>
    [DataElement(27, "Veterans Military Status", ElementUsage.Optional)]
    public CE VeteransMilitaryStatus { get; set; } = new();

    /// <summary>
    /// Gets or sets the Nationality (PID-28)
    /// </summary>
    [DataElement(28, "Nationality", ElementUsage.Optional)]
    public CE Nationality { get; set; } = new();

    /// <summary>
    /// Gets or sets the Patient Death Date and Time (PID-29)
    /// </summary>
    [DataElement(29, "Patient Death Date and Time", ElementUsage.Optional)]
    public string PatientDeathDateTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Patient Death Indicator (PID-30)
    /// </summary>
    [DataElement(30, "Patient Death Indicator", ElementUsage.Optional)]
    public string PatientDeathIndicator { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Identity Unknown Indicator (PID-31)
    /// </summary>
    [DataElement(31, "Identity Unknown Indicator", ElementUsage.Optional)]
    public string IdentityUnknownIndicator { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Identity Reliability Code (PID-32)
    /// </summary>
    [DataElement(32, "Identity Reliability Code", ElementUsage.Optional)]
    public string IdentityReliabilityCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Last Update Date/Time (PID-33)
    /// </summary>
    [DataElement(33, "Last Update Date/Time", ElementUsage.Optional)]
    public string LastUpdateDateTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Last Update Facility (PID-34)
    /// </summary>
    [DataElement(34, "Last Update Facility", ElementUsage.Optional)]
    public string LastUpdateFacility { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="PIDSegment"/> class
    /// </summary>
    public PIDSegment() : base("PID")
    {
    }
}
