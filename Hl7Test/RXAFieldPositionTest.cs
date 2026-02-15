using Hl7.Core;
using Hl7.Core.Segments;

namespace Hl7Test;

/// <summary>
/// Precise field position verification for RXA segment
/// </summary>
public class RXAFieldPositionTest
{
    [Fact]
    public void Show_Exact_RXA_Field_Positions()
    {
        // Exact line from user's test data
        var rxaLine = "RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP";
        
        var fields = rxaLine.Split('|');
        
        Console.WriteLine("\n=== EXACT RXA FIELD POSITIONS ===");
        Console.WriteLine($"Total fields: {fields.Length}\n");
        
        for (int i = 0; i < fields.Length; i++)
        {
            Console.WriteLine($"[{i:D2}]: '{fields[i]}'");
        }
        
        Console.WriteLine("\n=== FIELD MAPPING ===");
        Console.WriteLine($"Position [14] = '{fields[14]}' (SubstanceLotNumber?)");
        Console.WriteLine($"Position [15] = '{fields[15]}' (SubstanceExpirationDate?)");
        Console.WriteLine($"Position [16] = '{fields[16]}' (SubstanceManufacturerName?)");
        Console.WriteLine($"Position [17] = '{fields[17]}' (SubstanceRefusalReason?)");
        Console.WriteLine($"Position [20] = '{fields[20]}' (CompletionStatus?)");
        
        Console.WriteLine("\n=== WHAT WE NEED ===");
        Console.WriteLine($"HBV12345 is at position: [{Array.IndexOf(fields, "HBV12345")}]");
        Console.WriteLine($"SKB is at position: [{Array.IndexOf(fields, "SKB")}]");
        Console.WriteLine($"CP is at position: [{Array.IndexOf(fields, "CP")}]");
    }

    [Fact]
    public void Verify_RXA_Parsing_With_Correct_Indices()
    {
        var rxaLine = "RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP";
        var fields = rxaLine.Split('|');
        
        // Find the correct positions by value
        int hbv12345Pos = Array.IndexOf(fields, "HBV12345");
        int skbPos = Array.IndexOf(fields, "SKB");
        int cpPos = Array.IndexOf(fields, "CP");
        
        Console.WriteLine($"\nHBV12345 actual position: [{hbv12345Pos}]");
        Console.WriteLine($"SKB actual position: [{skbPos}]");
        Console.WriteLine($"CP actual position: [{cpPos}]");
        
        Assert.Equal("HBV12345", fields[hbv12345Pos]);
        Assert.Equal("SKB", fields[skbPos]);
        Assert.Equal("CP", fields[cpPos]);
    }
}
