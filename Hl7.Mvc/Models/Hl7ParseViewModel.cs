using Hl7.Core.Base;

namespace Hl7.Mvc.Models;

public class Hl7ParseViewModel
{
    public string Hl7Message { get; set; } = string.Empty;
    public Hl7Message? ParsedMessage { get; set; }
    public string? ErrorMessage { get; set; }
}
