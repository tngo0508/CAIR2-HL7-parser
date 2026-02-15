namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("ERR")]
public class ERRSegment : Segment
{
    [DataElement(1)]
    public string ErrorCodeAndLocation { get; set; } = string.Empty;

    [DataElement(2)]
    public string ErrorLocation { get; set; } = string.Empty;

    [DataElement(3)]
    public string Hl7ErrorCode { get; set; } = string.Empty;

    [DataElement(4)]
    public string Severity { get; set; } = string.Empty;

    public ERRSegment() : base("ERR")
    {
    }
}
