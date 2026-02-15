namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("ORC")]
public class ORCSegment : Segment
{
    [DataElement(1, "Order Control", ElementUsage.RequiredButMayBeEmpty)]
    public string OrderControl { get; set; } = string.Empty;

    [DataElement(2, "Placer Order Number", ElementUsage.RequiredButMayBeEmpty)]
    public string PlacerOrderNumber { get; set; } = string.Empty;

    [DataElement(3, "Filler Order Number", ElementUsage.RequiredButMayBeEmpty)]
    public string FillerOrderNumber { get; set; } = string.Empty;

    [DataElement(10, "Entered By", ElementUsage.RequiredButMayBeEmpty)]
    public string EnteredBy { get; set; } = string.Empty;

    [DataElement(12, "Ordering Provider", ElementUsage.RequiredButMayBeEmpty)]
    public string OrderingProvider { get; set; } = string.Empty;

    [DataElement(17, "Entering Organization", ElementUsage.RequiredButMayBeEmpty)]
    public string EnteringOrganization { get; set; } = string.Empty;

    public ORCSegment() : base("ORC")
    {
    }
}
