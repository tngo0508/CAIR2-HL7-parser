namespace Hl7.Core.Utils;

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

public static class ElementUsageExtensions
{
    public static string ToHl7Code(this ElementUsage usage) => usage switch
    {
        ElementUsage.Required => "R",
        ElementUsage.RequiredButMayBeEmpty => "RE",
        ElementUsage.Optional => "O",
        ElementUsage.Conditional => "C",
        ElementUsage.NotSupported => "X",
        _ => throw new ArgumentOutOfRangeException(nameof(usage), usage, "Unknown usage.")
    };

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
