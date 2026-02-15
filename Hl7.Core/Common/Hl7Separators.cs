namespace Hl7.Core.Common;

/// <summary>
/// Represents the separators used in an HL7 message
/// </summary>
public class Hl7Separators
{
    /// <summary>
    /// Gets or sets the field separator (typically '|')
    /// </summary>
    public char FieldSeparator { get; set; } = '|';

    /// <summary>
    /// Gets or sets the component separator (typically '^')
    /// </summary>
    public char ComponentSeparator { get; set; } = '^';

    /// <summary>
    /// Gets or sets the repetition separator (typically '~')
    /// </summary>
    public char RepetitionSeparator { get; set; } = '~';

    /// <summary>
    /// Gets or sets the escape character (typically '\')
    /// </summary>
    public char EscapeCharacter { get; set; } = '\\';

    /// <summary>
    /// Gets or sets the sub-component separator (typically '&amp;')
    /// </summary>
    public char SubComponentSeparator { get; set; } = '&';

    /// <summary>
    /// Parses the separators from an MSH segment
    /// </summary>
    /// <param name="mshSegment">The MSH segment line</param>
    /// <returns>A new <see cref="Hl7Separators"/> instance</returns>
    public static Hl7Separators ParseFromMSH(string mshSegment)
    {
        if (mshSegment.Length < 4 || !mshSegment.StartsWith("MSH"))
            throw new ArgumentException("Invalid MSH segment");

        return new Hl7Separators
        {
            FieldSeparator = mshSegment[3],
            ComponentSeparator = mshSegment.Length > 4 ? mshSegment[4] : '^',
            RepetitionSeparator = mshSegment.Length > 5 ? mshSegment[5] : '~',
            EscapeCharacter = mshSegment.Length > 6 ? mshSegment[6] : '\\',
            SubComponentSeparator = mshSegment.Length > 7 ? mshSegment[7] : '&'
        };
    }
}