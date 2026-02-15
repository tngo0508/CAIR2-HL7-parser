using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

/// <summary>
/// QAK - Query Acknowledgment Segment
/// </summary>
[Segment("QAK")]
public class QAKSegment : Segment
{
    /// <summary>
    /// Gets or sets the query tag (QAK-1)
    /// </summary>
    [DataElement(1, "Query Tag", ElementUsage.Required)]
    public string QueryTag { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the query response status (QAK-2)
    /// </summary>
    [DataElement(2, "Query Response Status", ElementUsage.Required)]
    public string QueryResponseStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message query name (QAK-3)
    /// </summary>
    [DataElement(3, "Message Query Name", ElementUsage.Required)]
    public string MessageQueryName { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="QAKSegment"/> class
    /// </summary>
    public QAKSegment() : base("QAK")
    {
    }
}
