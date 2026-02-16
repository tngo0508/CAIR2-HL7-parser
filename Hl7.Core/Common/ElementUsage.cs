namespace Hl7.Core.Common;

/// <summary>
/// HL7 element usage requirement (often shown as Usage codes: R, RE, O, C).
/// </summary>
public enum ElementUsage
{
    /// <summary>
    /// Required (R): must be populated with a non-empty value.
    /// </summary>
    Required,

    /// <summary>
    /// Required but may be empty (RE): must be sent if relevant data exists; may be omitted if unknown.
    /// </summary>
    RequiredButMayBeEmpty,

    /// <summary>
    /// Optional (O): may be sent if available.
    /// </summary>
    Optional,

    /// <summary>
    /// Conditional (C): requirement depends on a condition predicate.
    /// </summary>
    Conditional,

    /// <summary>
    /// Not supported (X): should not be populated.
    /// </summary>
    NotSupported
}

/// <summary>
/// Extension methods for the <see cref="ElementUsage"/> enum
/// </summary>
public static class ElementUsageExtensions
{
    /// <summary>
    /// Converts an <see cref="ElementUsage"/> value to its HL7 code representation
    /// </summary>
    /// <param name="usage">The element usage</param>
    /// <returns>The HL7 code (R, RE, O, C, or X)</returns>
    public static string ToHl7Code(this ElementUsage usage) => usage switch
    {
        ElementUsage.Required => "R",
        ElementUsage.RequiredButMayBeEmpty => "RE",
        ElementUsage.Optional => "O",
        ElementUsage.Conditional => "C",
        ElementUsage.NotSupported => "X",
        _ => throw new ArgumentOutOfRangeException(nameof(usage), usage, "Unknown usage.")
    };

    /// <summary>
    /// Converts an HL7 code to its <see cref="ElementUsage"/> enum value
    /// </summary>
    /// <param name="code">The HL7 code</param>
    /// <returns>The <see cref="ElementUsage"/> enum value</returns>
    public static ElementUsage FromHl7Code(string code) => code switch
    {
        "R" => ElementUsage.Required,
        "RE" => ElementUsage.RequiredButMayBeEmpty,
        "O" => ElementUsage.Optional,
        "C" => ElementUsage.Conditional,
        "X" => ElementUsage.NotSupported,
        _ => throw new ArgumentException($"Unknown HL7 usage code: '{code}'.", nameof(code))
    };
}
