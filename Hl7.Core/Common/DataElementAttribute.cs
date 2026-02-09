namespace Hl7.Core.Utils;

public class DataElementAttribute : Attribute
{
    public int Position { get; set; }
    public ElementUsage Status { get; set; }

    public DataElementAttribute(int position)
    {
        Position = position;
        Status = ElementUsage.Optional;
    }
}