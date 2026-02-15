using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

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
    public string PatientIdentifierList { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Alternate Patient ID - PID (PID-4)
    /// </summary>
    [DataElement(4, "Alternate Patient ID - PID", ElementUsage.Optional)]
    public string AlternatePatientId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Patient Name (PID-5)
    /// </summary>
    [DataElement(5, "Patient Name", ElementUsage.Optional)]
    public string PatientName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Mother's Maiden Name (PID-6)
    /// </summary>
    [DataElement(6, "Mother's Maiden Name", ElementUsage.Optional)]
    public string MothersMaidenName { get; set; } = string.Empty;

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
    /// Gets or sets the Race (PID-10)
    /// </summary>
    [DataElement(10, "Race", ElementUsage.Optional)]
    public string Race { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Patient Address (PID-11)
    /// </summary>
    [DataElement(11, "Patient Address", ElementUsage.Optional)]
    public string PatientAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the County Code (PID-12)
    /// </summary>
    [DataElement(12, "County Code", ElementUsage.Optional)]
    public string CountyCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Phone Number - Home (PID-13)
    /// </summary>
    [DataElement(13, "Phone Number - Home", ElementUsage.Optional)]
    public string PhoneNumberHome { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Phone Number - Business (PID-14)
    /// </summary>
    [DataElement(14, "Phone Number - Business", ElementUsage.Optional)]
    public string PhoneNumberBusiness { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Primary Language (PID-15)
    /// </summary>
    [DataElement(15, "Primary Language", ElementUsage.Optional)]
    public string PrimaryLanguage { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="PIDSegment"/> class
    /// </summary>
    public PIDSegment() : base("PID")
    {
    }
}
