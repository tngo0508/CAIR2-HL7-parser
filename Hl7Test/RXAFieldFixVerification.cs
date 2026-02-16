using Hl7.Core;
using Hl7.Core.Segments;

namespace Hl7Test;

/// <summary>
/// Verify RXA field parsing after the fix
/// </summary>
public class RXAFieldFixVerification
{
    [Fact]
    public void Verify_RXA_SubstanceLotNumber_Extraction()
    {
        // This is the exact line from the test that was failing
        var rxaLine = "RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP";
        
        var parser = new Hl7Parser();
        
        // Must parse MSH first to set separators
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        parser.ParseMSHSegment(mshLine);
        
        var rxa = parser.ParseSegment(rxaLine) as RXASegment;
        
        Assert.NotNull(rxa);
        Console.WriteLine("=== RXA FIELD VERIFICATION ===");
        Console.WriteLine($"GiveSubIdCounter: {rxa.GiveSubIdCounter} (expected: 0)");
        Console.WriteLine($"AdministrationSubIdCounter: '{rxa.AdministrationSubIdCounter}' (expected: '1')");
        Console.WriteLine($"DateTimeOfAdministration: '{rxa.DateTimeOfAdministration}' (expected: '20170101')");
        Console.WriteLine($"AdministeredCode: '{rxa.AdministeredCode}' (expected: '08^HepBPeds^CVX')");
        Console.WriteLine($"AdministeredAmount: '{rxa.AdministeredAmount}' (expected: '1.0')");
        Console.WriteLine($"AdministeredAtLocation: '{rxa.AdministeredAtLocation}' (expected: '^^^VICTORIATEST')");
        Console.WriteLine($"SubstanceLotNumber: '{rxa.SubstanceLotNumber}' (expected: 'HBV12345')");
        Console.WriteLine($"SubstanceManufacturerName: '{rxa.SubstanceManufacturerName}' (expected: 'SKB')");
        Console.WriteLine($"CompletionStatus: '{rxa.CompletionStatus}' (expected: 'CP')");
        
        // The critical assertion that was failing
        Assert.NotNull(rxa);
        Assert.Equal(0, rxa.GiveSubIdCounter);
        Assert.Equal("1", rxa.AdministrationSubIdCounter);
        Assert.Equal("20170101", rxa.DateTimeOfAdministration);
        Assert.Equal("08^HepBPeds^CVX", rxa.AdministeredCode);
        Assert.Equal("1.0", rxa.AdministeredAmount);
        Assert.Equal("^^^VICTORIATEST", rxa.AdministeredAtLocation);
        Assert.Equal("HBV12345", rxa.SubstanceLotNumber);  // ✅ THIS WAS FAILING
        Assert.Equal("SKB", rxa.SubstanceManufacturerName);
        Assert.Equal("CP", rxa.CompletionStatus);
    }

    [Fact]
    public void Verify_RXA_Field_Array_Positions()
    {
        var rxaLine = "RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP";
        var fields = rxaLine.Split('|');
        
        Console.WriteLine("=== ARRAY POSITIONS ===");
        for (int i = 0; i < fields.Length && i < 22; i++)
        {
            Console.WriteLine($"fields[{i:D2}]: '{fields[i]}'");
        }
        
        // Verify HBV12345 is at correct position
        Assert.Equal("HBV12345", fields[15]);
        Assert.Equal("SKB", fields[17]);
        Assert.Equal("CP", fields[20]);
    }

    [Fact]
    public void Verify_Full_Extract_Administered_Vaccines_Test()
    {
        // This is the exact test that was failing
        var fullMessage = @"MSH|^~\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001
MSA|AA|200||0||0^Message Accepted^HL70357
QAK|40005|OK|Z44
QPD|Z44^Request Immunization History and Forecast^CDCPHINVS|40005||WALL^MIKE^^^^^L|WINDOWS^DOLLY|20170101|M|2222 ANYWHERE Way^^Fresno^CA^93726|^PRN^PH^^^555^5555382
PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0
PD1|||||||||||02|N||||A
ORC|RE||11171795
RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP
RXR|IM|RT
OBX|1|CE|38890-0^COMPONENT VACCINE TYPE^LN|1|45^HepB^CVX^90731^HepB^CPT||||||F
OBX|2|NM|30973-2^Dose number in series^LN|1|1||||||F
ORC|RE||11173468
RXA|0|1|20170301|20170301|20^DTaP^CVX|1.0|||01|||||||||||CP
RXR|IM|RT
OBX|3|CE|38890-0^COMPONENT VACCINE TYPE^LN|1|107^DTP/aP^CVX^90700^DTP/aP^CPT||||||F
OBX|4|NM|30973-2^Dose number in series^LN|1|1||||||F
ORC|RE||0
RXA|0|1|20170509|20170509|998^No Vaccine Administered^CVX|999";

        var parser = new Hl7Parser();
        var message = parser.ParseMessage(fullMessage);

        var rxaSegments = message.GetSegments<RXASegment>("RXA");

        Console.WriteLine("\n=== EXTRACT_ADMINISTERED_VACCINES_TEST ===");
        Assert.Equal(3, rxaSegments.Count);

        // First vaccine: HepB (this is the one that was failing)
        Console.WriteLine("RXA[0]:");
        Console.WriteLine($"  DateTimeOfAdministration: '{rxaSegments[0].DateTimeOfAdministration}' (expected: '20170101')");
        Console.WriteLine($"  AdministeredCode: '{rxaSegments[0].AdministeredCode}' (expected: '08^HepBPeds^CVX')");
        Console.WriteLine($"  AdministeredAmount: '{rxaSegments[0].AdministeredAmount}' (expected: '1.0')");
        Console.WriteLine($"  SubstanceLotNumber: '{rxaSegments[0].SubstanceLotNumber}' (expected: 'HBV12345')");
        Console.WriteLine($"  CompletionStatus: '{rxaSegments[0].CompletionStatus}' (expected: 'CP')");

        Assert.Equal("20170101", rxaSegments[0].DateTimeOfAdministration);
        Assert.Equal("08^HepBPeds^CVX", rxaSegments[0].AdministeredCode);
        Assert.Equal("1.0", rxaSegments[0].AdministeredAmount);
        Assert.Equal("HBV12345", rxaSegments[0].SubstanceLotNumber);  // ✅ CRITICAL ASSERTION
        Assert.Equal("CP", rxaSegments[0].CompletionStatus);

        // Second vaccine: DTaP
        Console.WriteLine("\nRXA[1]:");
        Console.WriteLine($"  DateTimeOfAdministration: '{rxaSegments[1].DateTimeOfAdministration}' (expected: '20170301')");
        Console.WriteLine($"  AdministeredCode: '{rxaSegments[1].AdministeredCode}' (expected: '20^DTaP^CVX')");
        Console.WriteLine($"  CompletionStatus: '{rxaSegments[1].CompletionStatus}' (expected: 'CP')");

        Assert.Equal("20170301", rxaSegments[1].DateTimeOfAdministration);
        Assert.Equal("20^DTaP^CVX", rxaSegments[1].AdministeredCode);
        Assert.Equal("CP", rxaSegments[1].CompletionStatus);

        // Third: No vaccine administered
        Console.WriteLine("\nRXA[2]:");
        Console.WriteLine($"  DateTimeOfAdministration: '{rxaSegments[2].DateTimeOfAdministration}' (expected: '20170509')");
        Console.WriteLine($"  AdministeredCode: '{rxaSegments[2].AdministeredCode}' (expected: '998^No Vaccine Administered^CVX')");

        Assert.Equal("20170509", rxaSegments[2].DateTimeOfAdministration);
        Assert.Equal("998^No Vaccine Administered^CVX", rxaSegments[2].AdministeredCode);
        
        Console.WriteLine("\n✅ All assertions passed!");
    }
}
