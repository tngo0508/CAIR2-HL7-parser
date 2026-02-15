using Hl7.Core;
using Hl7.Core.Segments;

namespace Hl7Test;

/// <summary>
/// Minimal test to verify MSH field parsing is working correctly
/// </summary>
public class MinimalMSHTest
{
    [Fact]
    public void Verify_MSH_Fields_Are_Extracted_Correctly()
    {
        // Arrange
        var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001";
        var parser = new Hl7Parser();

        // Act
        var msh = parser.ParseMSHSegment(mshLine);

        // Assert - Check each field with detailed error messages
        Assert.True(msh.SegmentId == "MSH", $"SegmentId mismatch: expected 'MSH', got '{msh.SegmentId}'");
        Assert.True(msh.SendingApplication == "CAIR IIS", $"SendingApplication mismatch: expected 'CAIR IIS', got '{msh.SendingApplication}'");
        Assert.True(msh.SendingFacility == "CAIR IIS", $"SendingFacility mismatch: expected 'CAIR IIS', got '{msh.SendingFacility}'");
        Assert.True(msh.ReceivingApplication == "", $"ReceivingApplication mismatch: expected '', got '{msh.ReceivingApplication}'");
        Assert.True(msh.ReceivingFacility == "DE000001", $"ReceivingFacility mismatch: expected 'DE000001', got '{msh.ReceivingFacility}'");
        Assert.True(msh.MessageDateTime == "20170509", $"MessageDateTime mismatch: expected '20170509', got '{msh.MessageDateTime}'");
        Assert.True(msh.Security == "", $"Security mismatch: expected '', got '{msh.Security}'");
        Assert.True(msh.MessageType == "RSP^K11^RSP_K11", $"MessageType mismatch: expected 'RSP^K11^RSP_K11', got '{msh.MessageType}'");
        Assert.True(msh.MessageControlId == "200", $"MessageControlId mismatch: expected '200', got '{msh.MessageControlId}'");
        Assert.True(msh.ProcessingId == "P", $"ProcessingId mismatch: expected 'P', got '{msh.ProcessingId}'");
        Assert.True(msh.VersionId == "2.5.1", $"VersionId mismatch: expected '2.5.1', got '{msh.VersionId}'");
    }

    [Fact]
    public void Verify_Full_Message_Parsing()
    {
        // Arrange
        var fullMessage = @"MSH|^~\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001
PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0
RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP
OBX|1|CE|38890-0^COMPONENT VACCINE TYPE^LN|1|45^HepB^CVX^90731^HepB^CPT||||||F";

        var parser = new Hl7Parser();

        // Act
        var message = parser.ParseMessage(fullMessage);

        // Assert
        Assert.NotNull(message);
        Assert.True(message.Segments.Count == 4, $"Expected 4 segments, got {message.Segments.Count}");

        var msh = message.GetSegment<MSHSegment>("MSH");
        Assert.NotNull(msh);
        Assert.True(msh.SendingApplication == "CAIR IIS", $"MSH.SendingApplication: expected 'CAIR IIS', got '{msh.SendingApplication}'");
        Assert.True(msh.VersionId == "2.5.1", $"MSH.VersionId: expected '2.5.1', got '{msh.VersionId}'");

        var pid = message.GetSegment<PIDSegment>("PID");
        Assert.NotNull(pid);
        Assert.True(pid.PatientName == "WALL^MIKE", $"PID.PatientName: expected 'WALL^MIKE', got '{pid.PatientName}'");
        Assert.True(pid.DateOfBirth == "20170101", $"PID.DateOfBirth: expected '20170101', got '{pid.DateOfBirth}'");

        var rxa = message.GetSegment<RXASegment>("RXA");
        Assert.NotNull(rxa);
        Assert.True(rxa.AdministeredCode == "08^HepBPeds^CVX", $"RXA.AdministeredCode: expected '08^HepBPeds^CVX', got '{rxa.AdministeredCode}'");

        var obx = message.GetSegment<OBXSegment>("OBX");
        Assert.NotNull(obx);
        Assert.True(obx.ObservationIdentifier == "38890-0^COMPONENT VACCINE TYPE^LN", $"OBX.ObservationIdentifier: expected '38890-0^COMPONENT VACCINE TYPE^LN', got '{obx.ObservationIdentifier}'");
    }
}
