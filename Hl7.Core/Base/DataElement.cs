namespace Hl7.Core.Base;

public class DataElement
{
    public string Value { get; set; } = string.Empty;

    public DataElement() { }

    public DataElement(string value)
    {
        Value = value;
    }
}