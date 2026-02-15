using Hl7.Core;
using Hl7.Core.Segments;

namespace Hl7Test;

/// <summary>
/// Ultra-simple test to show exact field values
/// </summary>
public class UltraSimpleTest
{
    [Fact]
    public void Show_Exact_MSH_Values()
    {
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        
        // Manual split to see exact values
        var fields = mshLine.Split('|');
        
        Console.WriteLine("\n=== EXACT FIELD VALUES ===");
        Console.WriteLine($"[0]: '{fields[0]}'");
        Console.WriteLine($"[1]: '{fields[1]}'");
        Console.WriteLine($"[2]: '{fields[2]}'");
        Console.WriteLine($"[3]: '{fields[3]}'");
        Console.WriteLine($"[4]: '{fields[4]}'");
        Console.WriteLine($"[5]: '{fields[5]}'");
        Console.WriteLine($"[6]: '{fields[6]}'");
        Console.WriteLine($"[7]: '{fields[7]}'");
        Console.WriteLine($"[8]: '{fields[8]}'");
        Console.WriteLine($"[9]: '{fields[9]}'");
        Console.WriteLine($"[10]: '{fields[10]}'");
        Console.WriteLine($"[11]: '{fields[11]}'");
        
        // Now test with parser
        var parser = new Hl7Parser();
        var msh = parser.ParseMSHSegment(mshLine);
        
        Console.WriteLine("\n=== PARSER OUTPUT ===");
        Console.WriteLine($"SegmentId: '{msh.SegmentId}'");
        Console.WriteLine($"SendingApplication: '{msh.SendingApplication}'");
        Console.WriteLine($"SendingFacility: '{msh.SendingFacility}'");
        Console.WriteLine($"ReceivingApplication: '{msh.ReceivingApplication}'");
        Console.WriteLine($"ReceivingFacility: '{msh.ReceivingFacility}'");
        Console.WriteLine($"MessageDateTime: '{msh.MessageDateTime}'");
        Console.WriteLine($"Security: '{msh.Security}'");
        Console.WriteLine($"MessageType: '{msh.MessageType}'");
        Console.WriteLine($"MessageControlId: '{msh.MessageControlId}'");
        Console.WriteLine($"ProcessingId: '{msh.ProcessingId}'");
        Console.WriteLine($"VersionId: '{msh.VersionId}'");
        
        Console.WriteLine("\n=== ASSERTIONS ===");
        Assert.Equal("MSH", msh.SegmentId);
        Assert.Equal("CAIR IIS", msh.SendingApplication);
        Assert.Equal("CAIR IIS", msh.SendingFacility);
        Assert.Equal("", msh.ReceivingApplication);
        Assert.Equal("DE000001", msh.ReceivingFacility);
        Assert.Equal("20170509", msh.MessageDateTime);
        Assert.Equal("", msh.Security);
        Assert.Equal("RSP^K11^RSP_K11", msh.MessageType);
        Assert.Equal("200", msh.MessageControlId);
        Assert.Equal("P", msh.ProcessingId);
        Assert.Equal("2.5.1", msh.VersionId);
    }

    [Fact]
    public void Show_Exact_PID_Values()
    {
        var pidLine = "PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0";
        
        // Manual split
        var fields = pidLine.Split('|');
        
        Console.WriteLine("\n=== PID FIELD VALUES ===");
        for (int i = 0; i < 15 && i < fields.Length; i++)
        {
            Console.WriteLine($"[{i}]: '{fields[i]}'");
        }
        
        // Test with parser (must parse MSH first)
        var parser = new Hl7Parser();
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        parser.ParseMSHSegment(mshLine);
        
        var pid = parser.ParseSegment(pidLine) as PIDSegment;
        
        Assert.NotNull(pid);
        Console.WriteLine("\n=== PARSER OUTPUT ===");
        Console.WriteLine($"SegmentId: '{pid.SegmentId}'");
        Console.WriteLine($"SetId: {pid.SetId}");
        Console.WriteLine($"PatientId: '{pid.PatientId}'");
        Console.WriteLine($"PatientIdentifierList: '{pid.PatientIdentifierList}'");
        Console.WriteLine($"AlternatePatientId: '{pid.AlternatePatientId}'");
        Console.WriteLine($"PatientName: '{pid.PatientName}'");
        Console.WriteLine($"MothersMaidenName: '{pid.MothersMaidenName}'");
        Console.WriteLine($"DateOfBirth: '{pid.DateOfBirth}'");
        Console.WriteLine($"AdministrativeSex: '{pid.AdministrativeSex}'");
        Console.WriteLine($"PatientAddress: '{pid.PatientAddress}'");
        Console.WriteLine($"PhoneNumberHome: '{pid.PhoneNumberHome}'");
        
        Console.WriteLine("\n=== ASSERTIONS ===");
        Assert.Equal("PID", pid.SegmentId);
        Assert.Equal(1, pid.SetId);
        Assert.Equal("", pid.PatientId);
        Assert.Equal("291235^^^ORA^SR", pid.PatientIdentifierList);
        Assert.Equal("", pid.AlternatePatientId);
        Assert.Equal("WALL^MIKE", pid.PatientName);
        Assert.Equal("WINDOW^DOLLY", pid.MothersMaidenName);
        Assert.Equal("20170101", pid.DateOfBirth);
        Assert.Equal("M", pid.AdministrativeSex);
        Assert.Equal("2222 ANYWHERE WAY^^FRESNO^CA^93726^^H", pid.PatientAddress);
        Assert.Equal("^PRN^H^^^555^7575382", pid.PhoneNumberHome);
    }
}
