namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("NK1")]
public class NK1Segment : Segment
{
    [DataElement(1)]
    public int SetId { get; set; }

    [DataElement(2)]
    public string Name { get; set; } = string.Empty;

    [DataElement(3)]
    public string Relationship { get; set; } = string.Empty;

    [DataElement(4)]
    public string Address { get; set; } = string.Empty;

    [DataElement(5)]
    public string PhoneNumber { get; set; } = string.Empty;

    public NK1Segment() : base("NK1")
    {
    }
}
