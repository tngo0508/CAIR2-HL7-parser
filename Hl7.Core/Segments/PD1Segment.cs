namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("PD1")]
public class PD1Segment : Segment
{
    [DataElement(11)]
    public string PublicityCode { get; set; } = string.Empty;

    [DataElement(12)]
    public string ProtectionIndicator { get; set; } = string.Empty;

    [DataElement(13)]
    public string ProtectionIndicatorEffectiveDate { get; set; } = string.Empty;

    [DataElement(16)]
    public string ImmunizationRegistryStatus { get; set; } = string.Empty;

    [DataElement(17)]
    public string ImmunizationRegistryStatusEffectiveDate { get; set; } = string.Empty;

    public PD1Segment() : base("PD1")
    {
    }
}
