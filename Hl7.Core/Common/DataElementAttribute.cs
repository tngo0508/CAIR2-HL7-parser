using Hl7.Core.Utils;

namespace Hl7.Core.Common;

/// <summary>
/// Attribute used to mark a property as an HL7 data element (field)
/// </summary>
public class DataElementAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the 1-based position of the field within the segment
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    /// Gets or sets the requirement status of the field
    /// </summary>
    public ElementUsage Status { get; set; }

    /// <summary>
    /// Gets or sets the descriptive name of the field
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataElementAttribute"/> class with a position
    /// </summary>
    /// <param name="position">The field position</param>
    public DataElementAttribute(int position)
    {
        Position = position;
        Status = ElementUsage.Optional;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataElementAttribute"/> class with a position and name
    /// </summary>
    /// <param name="position">The field position</param>
    /// <param name="name">The field name</param>
    public DataElementAttribute(int position, string name)
    {
        Position = position;
        Name = name;
        Status = ElementUsage.Optional;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataElementAttribute"/> class with a position and status
    /// </summary>
    /// <param name="position">The field position</param>
    /// <param name="status">The requirement status</param>
    public DataElementAttribute(int position, ElementUsage status)
    {
        Position = position;
        Status = status;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataElementAttribute"/> class with a position, name, and status
    /// </summary>
    /// <param name="position">The field position</param>
    /// <param name="name">The field name</param>
    /// <param name="status">The requirement status</param>
    public DataElementAttribute(int position, string name, ElementUsage status)
    {
        Position = position;
        Name = name;
        Status = status;
    }
}
