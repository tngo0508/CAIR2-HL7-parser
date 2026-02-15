using Hl7.Core;
using Hl7.Core.Segments;

namespace Hl7Test;

/// <summary>
/// Diagnostic tests to verify field parsing
/// </summary>
public class DiagnosticParsingTests
{
    [Fact]
    public void Debug_MSH_Field_Extraction()
    {
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        var parser = new Hl7Parser();
        
        var msh = parser.ParseMSHSegment(mshLine);
        
        Console.WriteLine("=== MSH DEBUG ===");
        Console.WriteLine($"SegmentId: '{msh.SegmentId}'");
        Console.WriteLine($"SendingApplication: '{msh.SendingApplication}' (expected: 'CAIR IIS')");
        Console.WriteLine($"SendingFacility: '{msh.SendingFacility}' (expected: 'CAIR IIS')");
        Console.WriteLine($"ReceivingApplication: '{msh.ReceivingApplication}' (expected: '')");
        Console.WriteLine($"ReceivingFacility: '{msh.ReceivingFacility}' (expected: 'DE000001')");
        Console.WriteLine($"MessageDateTime: '{msh.MessageDateTime}' (expected: '20170509')");
        Console.WriteLine($"MessageType: '{msh.MessageType}' (expected: 'RSP^K11^RSP_K11')");
        Console.WriteLine($"MessageControlId: '{msh.MessageControlId}' (expected: '200')");
        Console.WriteLine($"ProcessingId: '{msh.ProcessingId}' (expected: 'P')");
        Console.WriteLine($"VersionId: '{msh.VersionId}' (expected: '2.5.1')");
        
        // Manual field split for debugging
        var fields = mshLine.Split('|');
        Console.WriteLine($"\nField array length: {fields.Length}");
        for (int i = 0; i < fields.Length && i < 15; i++)
        {
            Console.WriteLine($"  fields[{i}] = '{fields[i]}'");
        }
    }

    [Fact]
    public void Debug_PID_Field_Extraction()
    {
        var pidLine = "PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0";
        var parser = new Hl7Parser();
        
        // Must parse MSH first to set separators
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        parser.ParseMSHSegment(mshLine);
        
        var pid = parser.ParseSegment(pidLine) as PIDSegment;
        
        Console.WriteLine("\n=== PID DEBUG ===");
        Console.WriteLine($"SegmentId: '{pid.SegmentId}'");
        Console.WriteLine($"SetId: {pid.SetId} (expected: 1)");
        Console.WriteLine($"PatientId: '{pid.PatientId}' (expected: '')");
        Console.WriteLine($"PatientIdentifierList: '{pid.PatientIdentifierList}' (expected: '291235^^^ORA^SR')");
        Console.WriteLine($"PatientName: '{pid.PatientName}' (expected: 'WALL^MIKE')");
        Console.WriteLine($"MothersMaidenName: '{pid.MothersMaidenName}' (expected: 'WINDOW^DOLLY')");
        Console.WriteLine($"DateOfBirth: '{pid.DateOfBirth}' (expected: '20170101')");
        Console.WriteLine($"AdministrativeSex: '{pid.AdministrativeSex}' (expected: 'M')");
        Console.WriteLine($"PatientAddress: '{pid.PatientAddress}' (expected: '2222 ANYWHERE WAY^^FRESNO^CA^93726^^H')");
        Console.WriteLine($"PhoneNumberHome: '{pid.PhoneNumberHome}' (expected: '^PRN^H^^^555^7575382')");
        
        // Manual field split for debugging
        var fields = pidLine.Split('|');
        Console.WriteLine($"\nField array length: {fields.Length}");
        for (int i = 0; i < fields.Length && i < 20; i++)
        {
            Console.WriteLine($"  fields[{i}] = '{fields[i]}'");
        }
    }

    [Fact]
    public void Debug_RXA_Field_Extraction()
    {
        var rxaLine = "RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP";
        var parser = new Hl7Parser();
        
        // Must parse MSH first to set separators
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        parser.ParseMSHSegment(mshLine);
        
        var rxa = parser.ParseSegment(rxaLine) as RXASegment;
        
        Console.WriteLine("\n=== RXA DEBUG ===");
        Console.WriteLine($"SegmentId: '{rxa.SegmentId}'");
        Console.WriteLine($"GiveSubIdCounter: {rxa.GiveSubIdCounter} (expected: 0)");
        Console.WriteLine($"AdministrationSubIdCounter: '{rxa.AdministrationSubIdCounter}' (expected: '1')");
        Console.WriteLine($"DateTimeOfAdministration: '{rxa.DateTimeOfAdministration}' (expected: '20170101')");
        Console.WriteLine($"AdministeredCode: '{rxa.AdministeredCode}' (expected: '08^HepBPeds^CVX')");
        Console.WriteLine($"AdministeredAmount: '{rxa.AdministeredAmount}' (expected: '1.0')");
        Console.WriteLine($"SubstanceLotNumber: '{rxa.SubstanceLotNumber}' (expected: 'HBV12345')");
        Console.WriteLine($"SubstanceManufacturerName: '{rxa.SubstanceManufacturerName}' (expected: 'SKB')");
        Console.WriteLine($"CompletionStatus: '{rxa.CompletionStatus}' (expected: 'CP')");
        
        // Manual field split for debugging
        var fields = rxaLine.Split('|');
        Console.WriteLine($"\nField array length: {fields.Length}");
        for (int i = 0; i < fields.Length && i < 20; i++)
        {
            Console.WriteLine($"  fields[{i}] = '{fields[i]}'");
        }
    }

    [Fact]
    public void Debug_OBX_Field_Extraction()
    {
        var obxLine = "OBX|1|CE|38890-0^COMPONENT VACCINE TYPE^LN|1|45^HepB^CVX^90731^HepB^CPT||||||F";
        var parser = new Hl7Parser();
        
        // Must parse MSH first to set separators
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        parser.ParseMSHSegment(mshLine);
        
        var obx = parser.ParseSegment(obxLine) as OBXSegment;
        
        Console.WriteLine("\n=== OBX DEBUG ===");
        Console.WriteLine($"SegmentId: '{obx.SegmentId}'");
        Console.WriteLine($"SetId: {obx.SetId} (expected: 1)");
        Console.WriteLine($"ValueType: '{obx.ValueType}' (expected: 'CE')");
        Console.WriteLine($"ObservationIdentifier: '{obx.ObservationIdentifier}' (expected: '38890-0^COMPONENT VACCINE TYPE^LN')");
        Console.WriteLine($"ObservationSubId: '{obx.ObservationSubId}' (expected: '1')");
        Console.WriteLine($"ObservationValue: '{obx.ObservationValue}' (expected: '45^HepB^CVX^90731^HepB^CPT')");
        Console.WriteLine($"ObservationResultStatus: '{obx.ObservationResultStatus}' (expected: 'F')");
        
        // Manual field split for debugging
        var fields = obxLine.Split('|');
        Console.WriteLine($"\nField array length: {fields.Length}");
        for (int i = 0; i < fields.Length && i < 15; i++)
        {
            Console.WriteLine($"  fields[{i}] = '{fields[i]}'");
        }
    }
}
