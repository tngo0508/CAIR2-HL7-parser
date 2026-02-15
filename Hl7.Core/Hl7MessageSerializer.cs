namespace Hl7.Core;

using System.Reflection;
using Hl7.Core.Base;
using Hl7.Core.Segments;
using Hl7.Core.Utils;

/// <summary>
/// Serializes Hl7Message objects back to HL7 string format
/// </summary>
public class Hl7MessageSerializer
{
    private readonly Hl7Separators _separators;

    public Hl7MessageSerializer(Hl7Separators separators)
    {
        _separators = separators ?? throw new ArgumentNullException(nameof(separators));
    }

    /// <summary>
    /// Serializes an Hl7Message to HL7 string format
    /// </summary>
    public string Serialize(Hl7Message message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        var lines = new List<string>();

        foreach (var segment in message.Segments)
        {
            var segmentLine = SerializeSegment(segment);
            if (!string.IsNullOrEmpty(segmentLine))
            {
                lines.Add(segmentLine);
            }
        }

        return string.Join("\r\n", lines);
    }

    /// <summary>
    /// Serializes a single segment to HL7 string format
    /// </summary>
    public string SerializeSegment(Segment segment)
    {
        if (segment == null)
            throw new ArgumentNullException(nameof(segment));

        var fields = new List<string> { segment.SegmentId };

        if (segment is MSHSegment mshSegment)
        {
            fields.Add(_separators.ComponentSeparator.ToString() +
                      _separators.RepetitionSeparator.ToString() +
                      _separators.EscapeCharacter.ToString() +
                      _separators.SubComponentSeparator.ToString());
        }

        // Get all properties with DataElement attribute
        var properties = segment.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetCustomAttribute<DataElementAttribute>() != null)
            .OrderBy(p => p.GetCustomAttribute<DataElementAttribute>()!.Position)
            .ToList();

        foreach (var property in properties)
        {
            var value = property.GetValue(segment);
            var fieldValue = value == null ? string.Empty : value.ToString() ?? string.Empty;
            fields.Add(EscapeFieldValue(fieldValue));
        }

        // Remove trailing empty fields
        while (fields.Count > 1 && string.IsNullOrEmpty(fields[^1]))
        {
            fields.RemoveAt(fields.Count - 1);
        }

        return string.Join(_separators.FieldSeparator.ToString(), fields);
    }

    private string EscapeFieldValue(string value)
    {
        return Hl7FieldHelper.EscapeField(value, _separators);
    }
}
