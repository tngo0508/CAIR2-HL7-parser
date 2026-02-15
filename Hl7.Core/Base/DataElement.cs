namespace Hl7.Core.Base;

/// <summary>
/// Represents a simple HL7 data element
/// </summary>
public class DataElement
{
    /// <summary>
    /// Gets or sets the value of the data element
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataElement"/> class
    /// </summary>
    public DataElement() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataElement"/> class with a value
    /// </summary>
    /// <param name="value">The value of the data element</param>
    public DataElement(string value)
    {
        Value = value;
    }
}