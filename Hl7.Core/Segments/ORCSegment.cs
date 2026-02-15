using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;
using Hl7.Core.Types;

namespace Hl7.Core.Segments;

/// <summary>
/// ORC - Common Order Segment
/// </summary>
[Segment("ORC")]
public class ORCSegment : Segment
{
    /// <summary>
    /// Gets or sets the order control (ORC-1)
    /// </summary>
    [DataElement(1, "Order Control", ElementUsage.RequiredButMayBeEmpty)]
    public string OrderControl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the placer order number (ORC-2)
    /// </summary>
    [DataElement(2, "Placer Order Number", ElementUsage.RequiredButMayBeEmpty)]
    public string PlacerOrderNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the filler order number (ORC-3)
    /// </summary>
    [DataElement(3, "Filler Order Number", ElementUsage.RequiredButMayBeEmpty)]
    public string FillerOrderNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person who entered the order (ORC-10)
    /// </summary>
    [DataElement(10, "Entered By", ElementUsage.RequiredButMayBeEmpty)]
    public XPN EnteredBy { get; set; } = new();

    /// <summary>
    /// Gets or sets the ordering provider (ORC-12)
    /// </summary>
    [DataElement(12, "Ordering Provider", ElementUsage.RequiredButMayBeEmpty)]
    public XPN OrderingProvider { get; set; } = new();

    /// <summary>
    /// Gets or sets the entering organization (ORC-17)
    /// </summary>
    [DataElement(17, "Entering Organization", ElementUsage.RequiredButMayBeEmpty)]
    public CE EnteringOrganization { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ORCSegment"/> class
    /// </summary>
    public ORCSegment() : base("ORC")
    {
    }
}
