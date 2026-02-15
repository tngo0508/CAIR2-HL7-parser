namespace Hl7.Core.Common;

public class Hl7Separators
{
    public char FieldSeparator { get; set; } = '|';
    public char ComponentSeparator { get; set; } = '^';
    public char RepetitionSeparator { get; set; } = '~';
    public char EscapeCharacter { get; set; } = '\\';
    public char SubComponentSeparator { get; set; } = '&';

    public static Hl7Separators ParseFromMSH(string mshSegment)
    {
        if (mshSegment.Length < 4 || !mshSegment.StartsWith("MSH"))
            throw new ArgumentException("Invalid MSH segment");

        return new Hl7Separators
        {
            FieldSeparator = mshSegment[3],
            ComponentSeparator = mshSegment[4],
            RepetitionSeparator = mshSegment[5],
            EscapeCharacter = mshSegment[6],
            SubComponentSeparator = mshSegment[7]
        };
    }
}