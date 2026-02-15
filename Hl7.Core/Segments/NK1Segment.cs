namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("NK1")]
public class NK1Segment : Segment
{
    [DataElement(1, "Set ID - NK1", ElementUsage.Optional)]
    public int SetId { get; set; }

    [DataElement(2, "Name", ElementUsage.Optional)]
    public string Name { get; set; } = string.Empty;

    [DataElement(3, "Relationship", ElementUsage.Optional)]
    public string Relationship { get; set; } = string.Empty;

    [DataElement(4, "Address", ElementUsage.Optional)]
    public string Address { get; set; } = string.Empty;

    [DataElement(5, "Phone Number", ElementUsage.Optional)]
    public string PhoneNumber { get; set; } = string.Empty;

    public NK1Segment() : base("NK1")
    {
    }
}
