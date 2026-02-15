using System.Reflection;
using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Segments;
using Hl7.Core.Utils;

namespace Hl7.Core;

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

        // Get all properties with DataElement attribute
        var properties = segment.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetCustomAttribute<DataElementAttribute>() != null)
            .Select(p => new
            {
                Property = p,
                Attribute = p.GetCustomAttribute<DataElementAttribute>()!
            })
            .ToList();

        if (properties.Count == 0)
            return segment.SegmentId;

        var maxPosition = properties.Max(p => p.Attribute.Position);
        var fieldCount = segment is MSHSegment ? Math.Max(0, maxPosition - 1) : maxPosition;

        var fields = new List<string>(new string[fieldCount + 1]);
        fields[0] = segment.SegmentId;

        foreach (var entry in properties)
        {
            var position = entry.Attribute.Position;
            var index = segment is MSHSegment ? position - 1 : position;
            if (index <= 0 || index >= fields.Count)
                continue;

            var value = entry.Property.GetValue(segment);
            var fieldValue = value == null ? string.Empty : value.ToString() ?? string.Empty;

            if (segment is MSHSegment &&
                entry.Property.Name == nameof(MSHSegment.EncodingCharacters) &&
                string.IsNullOrEmpty(fieldValue))
            {
                fieldValue = _separators.ComponentSeparator.ToString() +
                             _separators.RepetitionSeparator.ToString() +
                             _separators.EscapeCharacter.ToString() +
                             _separators.SubComponentSeparator.ToString();
            }

            fields[index] = EscapeFieldValue(fieldValue);
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
