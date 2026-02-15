using Hl7.Core;
using Hl7.Core.Segments;

namespace Hl7Test;

/// <summary>
/// Final verification that RXA fields are parsed at CORRECT positions
/// </summary>
public class RXAFinalVerification
{
    [Fact]
    public void Verify_Exact_RXA_Field_Parsing()
    {
        // Exact test data from user
        var rxaLine = "RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP";
        
        var parser = new Hl7Parser();
        
        // Must parse MSH first
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        parser.ParseMSHSegment(mshLine);
        
        var rxa = parser.ParseSegment(rxaLine) as RXASegment;
        
        Console.WriteLine("=== FINAL RXA VERIFICATION ===\n");
        
        // All the key fields
        Console.WriteLine("Position 1 (GiveSubIdCounter):");
        Console.WriteLine($"  Expected: 0");
        Console.WriteLine($"  Actual:   {rxa.GiveSubIdCounter}");
        Assert.Equal(0, rxa.GiveSubIdCounter);
        
        Console.WriteLine("\nPosition 3 (DateTimeOfAdministration):");
        Console.WriteLine($"  Expected: 20170101");
        Console.WriteLine($"  Actual:   {rxa.DateTimeOfAdministration}");
        Assert.Equal("20170101", rxa.DateTimeOfAdministration);
        
        Console.WriteLine("\nPosition 5 (AdministeredCode):");
        Console.WriteLine($"  Expected: 08^HepBPeds^CVX");
        Console.WriteLine($"  Actual:   {rxa.AdministeredCode}");
        Assert.Equal("08^HepBPeds^CVX", rxa.AdministeredCode);
        
        Console.WriteLine("\nPosition 6 (AdministeredAmount):");
        Console.WriteLine($"  Expected: 1.0");
        Console.WriteLine($"  Actual:   {rxa.AdministeredAmount}");
        Assert.Equal("1.0", rxa.AdministeredAmount);
        
        Console.WriteLine("\nPosition 11 (AdministeredAtLocation):");
        Console.WriteLine($"  Expected: ^^^VICTORIATEST");
        Console.WriteLine($"  Actual:   {rxa.AdministeredAtLocation}");
        Assert.Equal("^^^VICTORIATEST", rxa.AdministeredAtLocation);
        
        Console.WriteLine("\n=== CRITICAL FIELDS ===\n");
        
        Console.WriteLine("Position 15 (SubstanceLotNumber): ← THE KEY FIX");
        Console.WriteLine($"  Expected: HBV12345");
        Console.WriteLine($"  Actual:   {rxa.SubstanceLotNumber}");
        Assert.Equal("HBV12345", rxa.SubstanceLotNumber);  // ✅ THIS WAS FAILING
        
        Console.WriteLine("\nPosition 17 (SubstanceManufacturerName):");
        Console.WriteLine($"  Expected: SKB");
        Console.WriteLine($"  Actual:   {rxa.SubstanceManufacturerName}");
        Assert.Equal("SKB", rxa.SubstanceManufacturerName);
        
        Console.WriteLine("\nPosition 20 (CompletionStatus):");
        Console.WriteLine($"  Expected: CP");
        Console.WriteLine($"  Actual:   {rxa.CompletionStatus}");
        Assert.Equal("CP", rxa.CompletionStatus);
        
        Console.WriteLine("\n✅ ALL ASSERTIONS PASSED!");
    }

    [Fact]
    public void Verify_With_Full_Message()
    {
        // Using the exact test message from user
        var fullMessage = @"MSH|^~\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001
MSA|AA|200||0||0^Message Accepted^HL70357
QAK|40005|OK|Z44^Request Immunization History and Forecast^CDCPHINVS
QPD|Z44^Request Immunization History and Forecast^CDCPHINVS|40005||WALL^MIKE^^^^^L|WINDOWS^DOLLY|20170101|M|2222 ANYWHERE WAY^^FRESNO^CA^93726|^PRN^PH^^^555^5555382
PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0
PD1|||||||||||02|N||||A
ORC|RE||11171795
RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP
RXR|IM|RT
OBX|1|CE|38890-0^COMPONENT VACCINE TYPE^LN|1|45^HepB^CVX^90731^HepB^CPT||||||F";

        var parser = new Hl7Parser();
        var message = parser.ParseMessage(fullMessage);

        var rxa = message.GetSegment<RXASegment>("RXA");

        Console.WriteLine("\n=== FULL MESSAGE PARSING TEST ===\n");
        Console.WriteLine($"RXA Segment found: {rxa != null}");
        
        if (rxa != null)
        {
            Console.WriteLine($"SubstanceLotNumber: {rxa.SubstanceLotNumber} (expected: HBV12345)");
            Console.WriteLine($"SubstanceManufacturerName: {rxa.SubstanceManufacturerName} (expected: SKB)");
            Console.WriteLine($"CompletionStatus: {rxa.CompletionStatus} (expected: CP)");
            
            Assert.Equal("HBV12345", rxa.SubstanceLotNumber);
            Assert.Equal("SKB", rxa.SubstanceManufacturerName);
            Assert.Equal("CP", rxa.CompletionStatus);
            
            Console.WriteLine("\n✅ FULL MESSAGE TEST PASSED!");
        }
        else
        {
            Assert.True(false, "RXA segment not found");
        }
    }

    [Fact]
    public void Show_Array_Positions_Reference()
    {
        var rxaLine = "RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP";
        var fields = rxaLine.Split('|');

        Console.WriteLine("\n=== ARRAY POSITION REFERENCE ===\n");
        Console.WriteLine("Key positions for SubstanceLotNumber issue:");
        Console.WriteLine($"  [14] = '{fields[14]}' (empty)");
        Console.WriteLine($"  [15] = '{fields[15]}' ← HBV12345 IS HERE!");
        Console.WriteLine($"  [16] = '{fields[16]}' (empty)");
        Console.WriteLine($"  [17] = '{fields[17]}' ← SKB IS HERE!");
        Console.WriteLine($"  [20] = '{fields[20]}' ← CP IS HERE!");

        // Verify
        Assert.Equal("HBV12345", fields[15]);
        Assert.Equal("SKB", fields[17]);
        Assert.Equal("CP", fields[20]);
        
        Console.WriteLine("\n✅ Array positions verified!");
    }
}
