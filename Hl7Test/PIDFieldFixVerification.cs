using Hl7.Core;
using Hl7.Core.Segments;

namespace Hl7Test;

/// <summary>
/// Verify PID field parsing fix
/// </summary>
public class PIDFieldFixVerification
{
    [Fact]
    public void Verify_PID_PatientAddress_Field()
    {
        // Exact test data
        var pidLine = "PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0";
        
        var parser = new Hl7Parser();
        
        // Must parse MSH first
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        parser.ParseMSHSegment(mshLine);
        
        var pid = parser.ParseSegment(pidLine) as PIDSegment;
        
        Console.WriteLine("=== PID FIELD VERIFICATION ===\n");
        
        Console.WriteLine("Position 1 (SetId):");
        Console.WriteLine($"  Expected: 1");
        Console.WriteLine($"  Actual:   {pid.SetId}");
        Assert.Equal(1, pid.SetId);
        
        Console.WriteLine("\nPosition 3 (PatientIdentifierList):");
        Console.WriteLine($"  Expected: 291235^^^ORA^SR");
        Console.WriteLine($"  Actual:   {pid.PatientIdentifierList}");
        Assert.Equal("291235^^^ORA^SR", pid.PatientIdentifierList);
        
        Console.WriteLine("\nPosition 5 (PatientName):");
        Console.WriteLine($"  Expected: WALL^MIKE");
        Console.WriteLine($"  Actual:   {pid.PatientName}");
        Assert.Equal("WALL^MIKE", pid.PatientName);
        
        Console.WriteLine("\nPosition 7 (DateOfBirth):");
        Console.WriteLine($"  Expected: 20170101");
        Console.WriteLine($"  Actual:   {pid.DateOfBirth}");
        Assert.Equal("20170101", pid.DateOfBirth);
        
        Console.WriteLine("\nPosition 8 (AdministrativeSex):");
        Console.WriteLine($"  Expected: M");
        Console.WriteLine($"  Actual:   {pid.AdministrativeSex}");
        Assert.Equal("M", pid.AdministrativeSex);
        
        Console.WriteLine("\n=== CRITICAL FIX ===\n");
        
        Console.WriteLine("Position 11 (PatientAddress): ← THE KEY FIX");
        Console.WriteLine($"  Expected: 2222 ANYWHERE WAY^^FRESNO^CA^93726^^H");
        Console.WriteLine($"  Actual:   {pid.PatientAddress}");
        Assert.Equal("2222 ANYWHERE WAY^^FRESNO^CA^93726^^H", pid.PatientAddress);  // ✅ THIS WAS FAILING
        
        Console.WriteLine("\nPosition 13 (PhoneNumberHome):");
        Console.WriteLine($"  Expected: ^PRN^H^^^555^7575382");
        Console.WriteLine($"  Actual:   {pid.PhoneNumberHome}");
        Assert.Equal("^PRN^H^^^555^7575382", pid.PhoneNumberHome);
        
        Console.WriteLine("\n✅ ALL ASSERTIONS PASSED!");
    }

    [Fact]
    public void Verify_Full_Extract_Patient_From_Real_Message_Test()
    {
        // This is the exact test that was failing
        var fullMessage = @"MSH|^~\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001
PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0";

        var parser = new Hl7Parser();
        var message = parser.ParseMessage(fullMessage);

        var pid = message.GetSegment<PIDSegment>("PID");

        Console.WriteLine("\n=== FULL MESSAGE PARSING TEST ===\n");
        
        Assert.NotNull(pid);
        
        Console.WriteLine($"PatientIdentifierList: {pid.PatientIdentifierList} (expected: 291235^^^ORA^SR)");
        Assert.Equal("291235^^^ORA^SR", pid.PatientIdentifierList);
        
        Console.WriteLine($"PatientName: {pid.PatientName} (expected: WALL^MIKE)");
        Assert.Equal("WALL^MIKE", pid.PatientName);
        
        Console.WriteLine($"DateOfBirth: {pid.DateOfBirth} (expected: 20170101)");
        Assert.Equal("20170101", pid.DateOfBirth);
        
        Console.WriteLine($"AdministrativeSex: {pid.AdministrativeSex} (expected: M)");
        Assert.Equal("M", pid.AdministrativeSex);
        
        Console.WriteLine($"PatientAddress: {pid.PatientAddress} (expected: 2222 ANYWHERE WAY^^FRESNO^CA^93726^^H)");
        Assert.Equal("2222 ANYWHERE WAY^^FRESNO^CA^93726^^H", pid.PatientAddress);
        
        Console.WriteLine("\n✅ EXTRACT_PATIENT_FROM_REAL_MESSAGE_TEST SHOULD NOW PASS!");
    }

    [Fact]
    public void Show_PID_Array_Positions_Reference()
    {
        var pidLine = "PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0";
        var fields = pidLine.Split('|');

        Console.WriteLine("\n=== PID ARRAY POSITION REFERENCE ===\n");
        Console.WriteLine("Key positions:");
        Console.WriteLine($"  [1]  = '{fields[1]}' (SetId = 1)");
        Console.WriteLine($"  [3]  = '{fields[3]}' (PatientIdentifierList = 291235^^^ORA^SR)");
        Console.WriteLine($"  [5]  = '{fields[5]}' (PatientName = WALL^MIKE)");
        Console.WriteLine($"  [7]  = '{fields[7]}' (DateOfBirth = 20170101)");
        Console.WriteLine($"  [8]  = '{fields[8]}' (AdministrativeSex = M)");
        Console.WriteLine($"  [10] = '{fields[10]}' (empty field)");
        Console.WriteLine($"  [11] = '{fields[11]}' ← PatientAddress IS HERE!");
        Console.WriteLine($"  [13] = '{fields[13]}' (PhoneNumberHome = ^PRN^H^^^555^7575382)");

        // Verify
        Assert.Equal("1", fields[1]);
        Assert.Equal("291235^^^ORA^SR", fields[3]);
        Assert.Equal("WALL^MIKE", fields[5]);
        Assert.Equal("20170101", fields[7]);
        Assert.Equal("M", fields[8]);
        Assert.Equal("", fields[10]);  // Empty field
        Assert.Equal("2222 ANYWHERE WAY^^FRESNO^CA^93726^^H", fields[11]);
        Assert.Equal("^PRN^H^^^555^7575382", fields[13]);
        
        Console.WriteLine("\n✅ Array positions verified!");
    }
}
