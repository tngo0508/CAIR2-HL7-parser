using Hl7.Core.Common;

namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("QPD")]
public class QPDSegment : Segment
{
    [DataElement(1, "Message Query Name", ElementUsage.Required)]
    public string MessageQueryName { get; set; } = string.Empty;

    [DataElement(2, "Query Tag", ElementUsage.Required)]
    public string QueryTag { get; set; } = string.Empty;

    [DataElement(3, "Patient List", ElementUsage.RequiredButMayBeEmpty)]
    public string PatientIdentifierList { get; set; } = string.Empty;

    [DataElement(4, "Patient Name", ElementUsage.Required)]
    public string PatientName { get; set; } = string.Empty;

    [DataElement(5, "Mother's Maiden Name", ElementUsage.RequiredButMayBeEmpty)]
    public string MothersMaidenName { get; set; } = string.Empty;

    [DataElement(6, "Date/Time of Birth", ElementUsage.Required)]
    public string DateOfBirth { get; set; } = string.Empty;

    [DataElement(7, "Administrative Sex", ElementUsage.Required)]
    public string AdministrativeSex { get; set; } = string.Empty;

    [DataElement(8, "Patient Address", ElementUsage.Required)]
    public string PatientAddress { get; set; } = string.Empty;

    [DataElement(9, "Phone Number - Home", ElementUsage.RequiredButMayBeEmpty)]
    public string PhoneNumberHome { get; set; } = string.Empty;

    [DataElement(10, "Multiple Birth Indicator", ElementUsage.RequiredButMayBeEmpty)]
    public string MultipleBirthIndicator { get; set; } = string.Empty;

    [DataElement(11, "Birth Order", ElementUsage.Conditional)]
    public string BirthOrder { get; set; } = string.Empty;

    public QPDSegment() : base("QPD")
    {
    }
}
