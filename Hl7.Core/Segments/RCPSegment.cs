using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

/// <summary>
/// RCP - Response Control Parameter Segment
/// </summary>
[Segment("RCP")]
public class RCPSegment : Segment
{
    /// <summary>
    /// Gets or sets the query priority (RCP-1)
    /// </summary>
    [DataElement(1, "Query Priority", ElementUsage.Required)]
    public string QueryPriority { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity limited request (RCP-2)
    /// </summary>
    [DataElement(2, "Quantity Limited Request", ElementUsage.Required)]
    public string QuantityLimitedRequest { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the response modality (RCP-3)
    /// </summary>
    [DataElement(3, "Response Modality", ElementUsage.Required)]
    public string ResponseModality { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="RCPSegment"/> class
    /// </summary>
    public RCPSegment() : base("RCP")
    {
    }
}
