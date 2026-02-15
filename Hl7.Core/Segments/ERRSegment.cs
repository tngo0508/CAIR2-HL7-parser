namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("ERR")]
public class ERRSegment : Segment
{
    [DataElement(1, "Error Code and Location", ElementUsage.Optional)]
    public string ErrorCodeAndLocation { get; set; } = string.Empty;

    [DataElement(2, "Error Location", ElementUsage.Optional)]
    public string ErrorLocation { get; set; } = string.Empty;

    [DataElement(3, "HL7 Error Code", ElementUsage.Optional)]
    public string Hl7ErrorCode { get; set; } = string.Empty;

    [DataElement(4, "Severity", ElementUsage.Optional)]
    public string Severity { get; set; } = string.Empty;

    public ERRSegment() : base("ERR")
    {
    }
}
