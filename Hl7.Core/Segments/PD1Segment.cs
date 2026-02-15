namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("PD1")]
public class PD1Segment : Segment
{
    [DataElement(11, "Publicity Code", ElementUsage.Optional)]
    public string PublicityCode { get; set; } = string.Empty;

    [DataElement(12, "Protection Indicator", ElementUsage.Optional)]
    public string ProtectionIndicator { get; set; } = string.Empty;

    [DataElement(13, "Protection Indicator Effective Date", ElementUsage.Optional)]
    public string ProtectionIndicatorEffectiveDate { get; set; } = string.Empty;

    [DataElement(16, "Immunization Registry Status", ElementUsage.Optional)]
    public string ImmunizationRegistryStatus { get; set; } = string.Empty;

    [DataElement(17, "Immunization Registry Status Effective Date", ElementUsage.Optional)]
    public string ImmunizationRegistryStatusEffectiveDate { get; set; } = string.Empty;

    public PD1Segment() : base("PD1")
    {
    }
}
