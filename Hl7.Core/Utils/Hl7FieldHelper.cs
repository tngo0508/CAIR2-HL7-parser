namespace Hl7.Core.Utils;

using Hl7.Core.Types;

/// <summary>
/// Utility class for HL7 field manipulation and parsing
/// </summary>
public static class Hl7FieldHelper
{
    /// <summary>
    /// Parses a composite field into components
    /// </summary>
    public static CompositeDataType ParseComposite(string value, Hl7Separators separators)
    {
        if (string.IsNullOrEmpty(value))
            return new CompositeDataType();
            
        return new CompositeDataType(UnescapeField(value, separators), separators.ComponentSeparator, separators.SubComponentSeparator);
    }

    /// <summary>
    /// Parses a repeating field into multiple values
    /// </summary>
    public static List<string> ParseRepeating(string value, Hl7Separators separators)
    {
        if (string.IsNullOrEmpty(value))
            return [];

        return value.Split(separators.RepetitionSeparator)
            .Select(v => UnescapeField(v, separators))
            .ToList();
    }

    /// <summary>
    /// Parses a repeating composite field
    /// </summary>
    public static List<CompositeDataType> ParseRepeatingComposite(string value, Hl7Separators separators)
    {
        if (string.IsNullOrEmpty(value))
            return [];

        return value.Split(separators.RepetitionSeparator)
            .Select(v => ParseComposite(v, separators))
            .ToList();
    }

    /// <summary>
    /// Escapes special characters in HL7 fields
    /// </summary>
    public static string EscapeField(string value, Hl7Separators separators)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return value
            .Replace(separators.EscapeCharacter.ToString(), 
                    separators.EscapeCharacter + "E" + separators.EscapeCharacter)
            .Replace(separators.FieldSeparator.ToString(), 
                    separators.EscapeCharacter + "F" + separators.EscapeCharacter)
            .Replace(separators.ComponentSeparator.ToString(), 
                    separators.EscapeCharacter + "S" + separators.EscapeCharacter)
            .Replace(separators.RepetitionSeparator.ToString(), 
                    separators.EscapeCharacter + "T" + separators.EscapeCharacter)
            .Replace(separators.SubComponentSeparator.ToString(), 
                    separators.EscapeCharacter + "C" + separators.EscapeCharacter);
    }

    /// <summary>
    /// Unescapes special characters in HL7 fields
    /// </summary>
    public static string UnescapeField(string value, Hl7Separators separators)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return value
            .Replace(separators.EscapeCharacter + "E" + separators.EscapeCharacter, 
                    separators.EscapeCharacter.ToString())
            .Replace(separators.EscapeCharacter + "F" + separators.EscapeCharacter, 
                    separators.FieldSeparator.ToString())
            .Replace(separators.EscapeCharacter + "S" + separators.EscapeCharacter, 
                    separators.ComponentSeparator.ToString())
            .Replace(separators.EscapeCharacter + "T" + separators.EscapeCharacter, 
                    separators.RepetitionSeparator.ToString())
            .Replace(separators.EscapeCharacter + "C" + separators.EscapeCharacter, 
                    separators.SubComponentSeparator.ToString());
    }

    /// <summary>
    /// Builds an HL7 message string from segments
    /// </summary>
    public static string BuildMessage(IEnumerable<string> segments)
    {
        return string.Join("\r\n", segments);
    }

    /// <summary>
    /// Formats a composite field with components
    /// </summary>
    public static string FormatComposite(params string[] components)
    {
        return string.Join("^", components.Where(c => !string.IsNullOrEmpty(c)));
    }
}
