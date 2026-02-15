using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;
using Hl7.Core.Types;

namespace Hl7.Core.Segments;

/// <summary>
/// NK1 - Next of Kin Segment
/// </summary>
[Segment("NK1")]
public class NK1Segment : Segment
{
    /// <summary>
    /// Gets or sets the Set ID - NK1 (NK1-1)
    /// </summary>
    [DataElement(1, "Set ID - NK1", ElementUsage.Optional)]
    public int SetId { get; set; }

    /// <summary>
    /// Gets or sets the Name (NK1-2)
    /// </summary>
    [DataElement(2, "Name", ElementUsage.Optional)]
    public XPN Name { get; set; } = new();

    /// <summary>
    /// Gets or sets the Relationship (NK1-3)
    /// </summary>
    [DataElement(3, "Relationship", ElementUsage.Optional)]
    public CE Relationship { get; set; } = new();

    /// <summary>
    /// Gets or sets the Address (NK1-4)
    /// </summary>
    [DataElement(4, "Address", ElementUsage.Optional)]
    public XAD Address { get; set; } = new();

    /// <summary>
    /// Gets or sets the Phone Number (NK1-5)
    /// </summary>
    [DataElement(5, "Phone Number", ElementUsage.Optional)]
    public XTN PhoneNumber { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="NK1Segment"/> class
    /// </summary>
    public NK1Segment() : base("NK1")
    {
    }
}
