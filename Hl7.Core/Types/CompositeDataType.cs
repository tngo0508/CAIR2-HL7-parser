namespace Hl7.Core.Types;

/// <summary>
/// Represents a composite data type with components and subcomponents
/// </summary>
public class CompositeDataType
{
    public List<string> Components { get; set; } = [];
    public Dictionary<int, List<string>> SubComponents { get; set; } = [];

    public CompositeDataType() { }

    public CompositeDataType(string value, char componentSeparator, char subComponentSeparator)
    {
        Parse(value, componentSeparator, subComponentSeparator);
    }

    public void Parse(string value, char componentSeparator, char subComponentSeparator)
    {
        if (string.IsNullOrEmpty(value))
            return;

        var components = value.Split(componentSeparator);
        Components = components.ToList();

        for (int i = 0; i < components.Length; i++)
        {
            var subComponents = components[i].Split(subComponentSeparator);
            SubComponents[i] = subComponents.ToList();
        }
    }

    public string GetComponent(int index) => 
        index < Components.Count ? Components[index] : string.Empty;

    public string GetSubComponent(int componentIndex, int subComponentIndex) =>
        SubComponents.ContainsKey(componentIndex) && 
        subComponentIndex < SubComponents[componentIndex].Count
            ? SubComponents[componentIndex][subComponentIndex]
            : string.Empty;

    public override string ToString() => string.Join("^", Components);
}
