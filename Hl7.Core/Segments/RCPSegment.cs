namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("RCP")]
public class RCPSegment : Segment
{
    [DataElement(1, "Query Priority", ElementUsage.Required)]
    public string QueryPriority { get; set; } = string.Empty;

    [DataElement(2, "Quantity Limited Request", ElementUsage.Required)]
    public string QuantityLimitedRequest { get; set; } = string.Empty;

    [DataElement(3, "Response Modality", ElementUsage.Required)]
    public string ResponseModality { get; set; } = string.Empty;

    public RCPSegment() : base("RCP")
    {
    }
}
