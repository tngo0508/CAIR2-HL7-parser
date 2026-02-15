using Hl7.Core.Utils;

namespace Hl7.Core.Common;

public class DataElementAttribute : Attribute
{
    public int Position { get; set; }
    public ElementUsage Status { get; set; }
    public string Name { get; set; } = string.Empty;

    public DataElementAttribute(int position)
    {
        Position = position;
        Status = ElementUsage.Optional;
    }

    public DataElementAttribute(int position, string name)
    {
        Position = position;
        Name = name;
        Status = ElementUsage.Optional;
    }

    public DataElementAttribute(int position, ElementUsage status)
    {
        Position = position;
        Status = status;
    }

    public DataElementAttribute(int position, string name, ElementUsage status)
    {
        Position = position;
        Name = name;
        Status = status;
    }
}
