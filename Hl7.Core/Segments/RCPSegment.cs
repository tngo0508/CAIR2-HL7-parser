namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("RCP")]
public class RCPSegment : Segment
{
    [DataElement(1)]
    public string QueryPriority { get; set; } = string.Empty;

    [DataElement(2)]
    public string QuantityLimitedRequest { get; set; } = string.Empty;

    [DataElement(3)]
    public string ResponseModality { get; set; } = string.Empty;

    public RCPSegment() : base("RCP")
    {
    }
}
