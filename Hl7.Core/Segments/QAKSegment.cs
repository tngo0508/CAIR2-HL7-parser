namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("QAK")]
public class QAKSegment : Segment
{
    [DataElement(1, "Query Tag", ElementUsage.Required)]
    public string QueryTag { get; set; } = string.Empty;

    [DataElement(2, "Query Response Status", ElementUsage.Required)]
    public string QueryResponseStatus { get; set; } = string.Empty;

    [DataElement(3, "Message Query Name", ElementUsage.Required)]
    public string MessageQueryName { get; set; } = string.Empty;

    public QAKSegment() : base("QAK")
    {
    }
}
