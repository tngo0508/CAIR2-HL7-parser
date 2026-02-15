using Hl7.Core;
using Hl7.Core.Segments;
using Xunit;

namespace Hl7Test;

public class SegmentElementExtractionTests
{
    [Fact]
    public void Test_GetElement_Extraction()
    {
        var parser = new Hl7Parser();
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1";
        var pidLine = "PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M";
        
        parser.ParseMSHSegment(mshLine);
        var pid = parser.ParseSegment(pidLine) as PIDSegment;
        
        Assert.NotNull(pid);
        
        // Test string extraction
        Assert.Equal("1", pid.GetField(1));
        Assert.Equal("1", pid.GetElement<string>(1));
        Assert.Equal("291235^^^ORA^SR", pid.GetElement<string>(3));
        Assert.Equal("WALL^MIKE", pid.GetElement<string>(5));
        
        // Test int extraction
        Assert.Equal(1, pid.GetElement<int>(1));
        
        // Test non-existent field
        Assert.Equal(string.Empty, pid.GetField(100));
        Assert.Null(pid.GetElement<string>(100));
        Assert.Equal(0, pid.GetElement<int>(100));
    }

    [Fact]
    public void Test_RXA_Element_Extraction()
    {
        var parser = new Hl7Parser();
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1";
        var rxaLine = "RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP";
        
        parser.ParseMSHSegment(mshLine);
        var rxa = parser.ParseSegment(rxaLine) as RXASegment;
        
        Assert.NotNull(rxa);
        
        // Test typed extraction vs properties
        Assert.Equal(rxa.GiveSubIdCounter, rxa.GetElement<int>(1));
        Assert.Equal(rxa.DateTimeOfAdministration, rxa.GetElement<string>(3));
        Assert.Equal(rxa.AdministeredCode, rxa.GetElement<string>(5));
        Assert.Equal(rxa.SubstanceLotNumber, rxa.GetElement<string>(15));
        Assert.Equal(rxa.CompletionStatus, rxa.GetElement<string>(20));
    }
}
