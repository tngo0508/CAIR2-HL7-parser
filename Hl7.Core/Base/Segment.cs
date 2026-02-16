namespace Hl7.Core.Base;

/// <summary>
/// Represents a generic HL7 segment
/// </summary>
public class Segment
{
    /// <summary>
    /// Gets or sets the segment identifier (e.g., MSH, PID)
    /// </summary>
    public string SegmentId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the dictionary of fields in the segment
    /// </summary>
    public Dictionary<int, string> Fields { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Segment"/> class
    /// </summary>
    public Segment() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Segment"/> class with a segment identifier
    /// </summary>
    /// <param name="segmentId">The segment identifier</param>
    public Segment(string segmentId)
    {
        SegmentId = segmentId;
    }

    /// <summary>
    /// Gets the raw value of a field at the specified position
    /// </summary>
    /// <param name="position">The 1-based position of the field</param>
    /// <returns>The field value, or an empty string if not found</returns>
    public string GetField(int position)
    {
        return Fields.TryGetValue(position, out var value) ? value : string.Empty;
    }

    /// <summary>
    /// Gets the value of a field at the specified position, converted to the specified type
    /// </summary>
    /// <typeparam name="T">The type to convert to</typeparam>
    /// <param name="position">The 1-based position of the field</param>
    /// <returns>The converted value, or the default value for the type if conversion fails</returns>
    public T? GetElement<T>(int position)
    {
        var value = GetField(position);
        if (string.IsNullOrEmpty(value)) return default;

        try
        {
            if (typeof(T) == typeof(string))
            {
                return (T)(object)value;
            }

            if (typeof(T) == typeof(int))
            {
                return int.TryParse(value, out var result) ? (T)(object)result : default;
            }

            if (typeof(T) == typeof(decimal))
            {
                return decimal.TryParse(value, out var result) ? (T)(object)result : default;
            }

            if (typeof(T) == typeof(DateTime))
            {
                return DateTime.TryParse(value, out var result) ? (T)(object)result : default;
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
        catch
        {
            return default;
        }
    }
}