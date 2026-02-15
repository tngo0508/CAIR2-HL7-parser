namespace Hl7.Core.Types;

/// <summary>
/// Represents a composite data type with components and subcomponents
/// </summary>
public class CompositeDataType
{
    /// <summary>
    /// Gets or sets the list of components in the composite field
    /// </summary>
    public List<string> Components { get; set; } = [];

    /// <summary>
    /// Gets or sets the dictionary of subcomponents for each component
    /// </summary>
    public Dictionary<int, List<string>> SubComponents { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeDataType"/> class
    /// </summary>
    public CompositeDataType() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeDataType"/> class by parsing a value
    /// </summary>
    /// <param name="value">The composite field value</param>
    /// <param name="componentSeparator">The component separator character</param>
    /// <param name="subComponentSeparator">The subcomponent separator character</param>
    public CompositeDataType(string value, char componentSeparator, char subComponentSeparator)
    {
        Parse(value, componentSeparator, subComponentSeparator);
    }

    /// <summary>
    /// Parses a composite field value into components and subcomponents
    /// </summary>
    /// <param name="value">The composite field value</param>
    /// <param name="componentSeparator">The component separator character</param>
    /// <param name="subComponentSeparator">The subcomponent separator character</param>
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

    /// <summary>
    /// Gets a component value by index
    /// </summary>
    /// <param name="index">The 0-based component index</param>
    /// <returns>The component value; or empty string if index is out of range</returns>
    public string GetComponent(int index) => 
        index < Components.Count ? Components[index] : string.Empty;

    /// <summary>
    /// Gets a subcomponent value by component index and subcomponent index
    /// </summary>
    /// <param name="componentIndex">The 0-based component index</param>
    /// <param name="subComponentIndex">The 0-based subcomponent index</param>
    /// <returns>The subcomponent value; or empty string if indices are out of range</returns>
    public string GetSubComponent(int componentIndex, int subComponentIndex) =>
        SubComponents.ContainsKey(componentIndex) && 
        subComponentIndex < SubComponents[componentIndex].Count
            ? SubComponents[componentIndex][subComponentIndex]
            : string.Empty;

    /// <summary>
    /// Returns the string representation of the composite data type
    /// </summary>
    /// <returns>The composite field string</returns>
    public override string ToString() => string.Join("^", Components);
}
