using Hl7.Core.Common;
using Hl7.Core.Base;
using Hl7.Core.Utils;
using Hl7.Core.Types;

namespace Hl7.Core.Segments;

/// <summary>
/// QPD - Query Parameter Definition Segment
/// </summary>
[Segment("QPD")]
public class QPDSegment : Segment
{
    /// <summary>
    /// Gets or sets the message query name (QPD-1)
    /// </summary>
    [DataElement(1, "Message Query Name", ElementUsage.Required)]
    public CE MessageQueryName { get; set; } = new();

    /// <summary>
    /// Gets or sets the query tag (QPD-2)
    /// </summary>
    [DataElement(2, "Query Tag", ElementUsage.Required)]
    public string QueryTag { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient identifier list (QPD-3)
    /// </summary>
    [DataElement(3, "Patient List", ElementUsage.RequiredButMayBeEmpty)]
    public CX PatientIdentifierList { get; set; } = new();

    /// <summary>
    /// Gets or sets the patient name (QPD-4)
    /// </summary>
    [DataElement(4, "Patient Name", ElementUsage.Required)]
    public XPN PatientName { get; set; } = new();

    /// <summary>
    /// Gets or sets the mother's maiden name (QPD-5)
    /// </summary>
    [DataElement(5, "Mother's Maiden Name", ElementUsage.RequiredButMayBeEmpty)]
    public XPN MothersMaidenName { get; set; } = new();

    /// <summary>
    /// Gets or sets the date/time of birth (QPD-6)
    /// </summary>
    [DataElement(6, "Date/Time of Birth", ElementUsage.Required)]
    public string DateOfBirth { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the administrative sex (QPD-7)
    /// </summary>
    [DataElement(7, "Administrative Sex", ElementUsage.Required)]
    public string AdministrativeSex { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient address (QPD-8)
    /// </summary>
    [DataElement(8, "Patient Address", ElementUsage.Required)]
    public XAD PatientAddress { get; set; } = new();

    /// <summary>
    /// Gets or sets the home phone number (QPD-9)
    /// </summary>
    [DataElement(9, "Phone Number - Home", ElementUsage.RequiredButMayBeEmpty)]
    public XTN PhoneNumberHome { get; set; } = new();

    /// <summary>
    /// Gets or sets the multiple birth indicator (QPD-10)
    /// </summary>
    [DataElement(10, "Multiple Birth Indicator", ElementUsage.RequiredButMayBeEmpty)]
    public string MultipleBirthIndicator { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the birth order (QPD-11)
    /// </summary>
    [DataElement(11, "Birth Order", ElementUsage.Conditional)]
    public string BirthOrder { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="QPDSegment"/> class
    /// </summary>
    public QPDSegment() : base("QPD")
    {
    }
}
