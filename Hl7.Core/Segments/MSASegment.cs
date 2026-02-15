namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("MSA")]
public class MSASegment : Segment
{
    [DataElement(1)]
    public string AcknowledgmentCode { get; set; } = string.Empty;

    [DataElement(2)]
    public string MessageControlId { get; set; } = string.Empty;

    [DataElement(3)]
    public string TextMessage { get; set; } = string.Empty;

    public MSASegment() : base("MSA")
    {
    }
}
