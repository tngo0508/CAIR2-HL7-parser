using Hl7.Core;
using Hl7.Core.Segments;

namespace Hl7Test;

/// <summary>
/// Diagnostic test for PID field parsing
/// </summary>
public class PIDFieldDiagnostic
{
    [Fact]
    public void Show_Exact_PID_Field_Positions()
    {
        // Exact PID line from test
        var pidLine = "PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0";
        
        var fields = pidLine.Split('|');
        
        Console.WriteLine("\n=== EXACT PID FIELD POSITIONS ===");
        Console.WriteLine($"Total fields: {fields.Length}\n");
        
        for (int i = 0; i < fields.Length && i < 20; i++)
        {
            Console.WriteLine($"[{i:D2}]: '{fields[i]}'");
        }
        
        Console.WriteLine("\n=== KEY FIELD MAPPING ===");
        Console.WriteLine($"Position [3]  = '{fields[3]}' (PatientIdentifierList?) - Expected: '291235^^^ORA^SR'");
        Console.WriteLine($"Position [5]  = '{fields[5]}' (PatientName?) - Expected: 'WALL^MIKE'");
        Console.WriteLine($"Position [7]  = '{fields[7]}' (DateOfBirth?) - Expected: '20170101'");
        Console.WriteLine($"Position [8]  = '{fields[8]}' (AdministrativeSex?) - Expected: 'M'");
        Console.WriteLine($"Position [10] = '{fields[10]}' (PatientAddress?) - Expected: '2222 ANYWHERE WAY^^FRESNO^CA^93726^^H' - ACTUAL: EMPTY!");
        Console.WriteLine($"Position [11] = '{fields[11]}' (PatientAddress?) - Expected: '2222 ANYWHERE WAY^^FRESNO^CA^93726^^H' - ACTUAL: YES!");
    }

    [Fact]
    public void Verify_PID_Parsing_With_Parser()
    {
        var pidLine = "PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0";
        
        var parser = new Hl7Parser();
        
        // Must parse MSH first
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        parser.ParseMSHSegment(mshLine);
        
        var pid = parser.ParseSegment(pidLine) as PIDSegment;
        
        Console.WriteLine("\n=== CURRENT PARSER OUTPUT ===");
        Console.WriteLine($"PatientIdentifierList: '{pid.PatientIdentifierList}' (expected: '291235^^^ORA^SR')");
        Console.WriteLine($"PatientName: '{pid.PatientName}' (expected: 'WALL^MIKE')");
        Console.WriteLine($"DateOfBirth: '{pid.DateOfBirth}' (expected: '20170101')");
        Console.WriteLine($"AdministrativeSex: '{pid.AdministrativeSex}' (expected: 'M')");
        Console.WriteLine($"PatientAddress: '{pid.PatientAddress}' (expected: '2222 ANYWHERE WAY^^FRESNO^CA^93726^^H')");
        Console.WriteLine($"PhoneNumberHome: '{pid.PhoneNumberHome}' (expected: '^PRN^H^^^555^7575382')");
        
        // Check which ones match
        Console.WriteLine("\n=== MATCHING ASSERTIONS ===");
        try { Assert.Equal("291235^^^ORA^SR", pid.PatientIdentifierList); Console.WriteLine("✓ PatientIdentifierList matches"); }
        catch { Console.WriteLine("✗ PatientIdentifierList DOES NOT match"); }
        
        try { Assert.Equal("WALL^MIKE", pid.PatientName); Console.WriteLine("✓ PatientName matches"); }
        catch { Console.WriteLine("✗ PatientName DOES NOT match"); }
        
        try { Assert.Equal("20170101", pid.DateOfBirth); Console.WriteLine("✓ DateOfBirth matches"); }
        catch { Console.WriteLine("✗ DateOfBirth DOES NOT match"); }
        
        try { Assert.Equal("M", pid.AdministrativeSex); Console.WriteLine("✓ AdministrativeSex matches"); }
        catch { Console.WriteLine("✗ AdministrativeSex DOES NOT match"); }
        
        try { Assert.Equal("2222 ANYWHERE WAY^^FRESNO^CA^93726^^H", pid.PatientAddress); Console.WriteLine("✓ PatientAddress matches"); }
        catch { Console.WriteLine("✗ PatientAddress DOES NOT match - THIS IS THE ISSUE"); }
    }
}
