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
    public static Hl7Separators ParseFromMSH(string headerSegment)
    {
        if (headerSegment.Length < 4 || (!headerSegment.StartsWith("MSH") && !headerSegment.StartsWith("FHS") && !headerSegment.StartsWith("BHS")))
            throw new ArgumentException("Invalid header segment");

        return new Hl7Separators
        {
            FieldSeparator = headerSegment[3],
            ComponentSeparator = headerSegment.Length > 4 ? headerSegment[4] : '^',
            RepetitionSeparator = headerSegment.Length > 5 ? headerSegment[5] : '~',
            EscapeCharacter = headerSegment.Length > 6 ? headerSegment[6] : '\\',
            SubComponentSeparator = headerSegment.Length > 7 ? headerSegment[7] : '&'
        };
    }
}