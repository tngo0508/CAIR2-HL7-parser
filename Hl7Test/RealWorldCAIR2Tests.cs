using Hl7.Core;
using Hl7.Core.CAIR2;
using Hl7.Core.Segments;
using Hl7.Core.Validation;
using Hl7.Core.Base;

namespace Hl7Test;

/// <summary>
/// Real-world CAIR2 RSP^K11 (Immunization History and Forecast Response) message testing
/// </summary>
public class RealWorldCAIR2Tests
{
    private const string RealCAIR2Message = @"MSH|^~\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|||||||||Z42^CDCPHINVS|CAIR IIS|DE000001
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
RXA|0|1|20170509|20170509|998^No Vaccine Administered^CVX|999
OBX|5|CE|30979-9^Vaccines Due Next^LN|0|107^DTP/aP^CVX^90700^DTP/aP^CPT||||||F
OBX|6|TS|30980-7^Date Vaccine Due^LN|0|20170501||||||F
OBX|7|NM|30973-2^Vaccine due next dose number^LN|0|2||||||F
OBX|8|TS|30981-5^Earliest date to give^LN|0|20170329||||||F
OBX|9|CE|30982-3^Reason applied by forecast logic to project this vaccine^LN|0|ACIP schedule||||||F
OBX|10|CE|30979-9^Vaccines Due Next^LN|1|85^HepA^CVX^90730^HepA^CPT||||||F
OBX|11|TS|30980-7^Date Vaccine Due^LN|1|20180101||||||F
OBX|12|NM|30973-2^Vaccine due next dose number^LN|1|1||||||F
OBX|13|TS|30981-5^Earliest date to give^LN|1|20180101||||||F
OBX|14|CE|30982-3^Reason applied by forecast logic to project this vaccine^LN|1|ACIP schedule||||||F
OBX|15|CE|30979-9^Vaccines Due Next^LN|2|45^HepB^CVX^90731^HepB^CPT||||||F
OBX|16|TS|30980-7^Date Vaccine Due^LN|2|20170301||||||F
OBX|17|NM|30973-2^Vaccine due next dose number^LN|2|2||||||F
OBX|18|TS|30981-5^Earliest date to give^LN|2|20170129||||||F
OBX|19|CE|30982-3^Reason applied by forecast logic to project this vaccine^LN|2|ACIP schedule||||||F
OBX|20|CE|30979-9^Vaccines Due Next^LN|3|17^Hib^CVX^90737^Hib^CPT||||||F
OBX|21|TS|30980-7^Date Vaccine Due^LN|3|20170301||||||F
OBX|22|NM|30973-2^Vaccine due next dose number^LN|3|1||||||F
OBX|23|TS|30981-5^Earliest date to give^LN|3|20170212||||||F
OBX|24|CE|30982-3^Reason applied by forecast logic to project this vaccine^LN|3|ACIP schedule||||||F
OBX|25|CE|30979-9^Vaccines Due Next^LN|4|88^Influenza-seasnl^CVX^90724^Influenzaseasnl^CPT||||||F
OBX|26|TS|30980-7^Date Vaccine Due^LN|4|20170701||||||F
OBX|27|NM|30973-2^Vaccine due next dose number^LN|4|1||||||F
OBX|28|TS|30981-5^Earliest date to give^LN|4|20170701||||||F
OBX|29|CE|30982-3^Reason applied by forecast logic to project this vaccine^LN|4|ACIP schedule||||||F
OBX|30|CE|30979-9^Vaccines Due Next^LN|5|03^MMR^CVX^90707^MMR^CPT||||||F
OBX|31|TS|30980-7^Date Vaccine Due^LN|5|20180101||||||F
OBX|32|NM|30973-2^Vaccine due next dose number^LN|5|0||||||F
OBX|33|TS|30981-5^Earliest date to give^LN|5|20180101||||||F
OBX|34|CE|30982-3^Reason applied by forecast logic to project this vaccine^LN|5|ACIP schedule||||||F
OBX|35|CE|30979-9^Vaccines Due Next^LN|6|133^PneumoConjugate^CVX^90670^PneumoConjugate^CPT||||||F
OBX|36|TS|30980-7^Date Vaccine Due^LN|6|20170301||||||F
OBX|37|NM|30973-2^Vaccine due next dose number^LN|6|1||||||F
OBX|38|TS|30981-5^Earliest date to give^LN|6|20170212||||||F
OBX|39|CE|30982-3^Reason applied by forecast logic to project this vaccine^LN|6|ACIP schedule||||||F
OBX|40|CE|30979-9^Vaccines Due Next^LN|7|89^Polio^CVX||||||F
OBX|41|TS|30980-7^Date Vaccine Due^LN|7|20170301||||||F
OBX|42|NM|30973-2^Vaccine due next dose number^LN|7|1||||||F
OBX|43|TS|30981-5^Earliest date to give^LN|7|20170212||||||F
OBX|44|CE|30982-3^Reason applied by forecast logic to project this vaccine^LN|7|ACIP schedule||||||F
OBX|45|CE|30979-9^Vaccines Due Next^LN|8|21^Varicella^CVX^90716^Varicella^CPT||||||F
OBX|46|TS|30980-7^Date Vaccine Due^LN|8|20180101||||||F
OBX|47|NM|30973-2^Vaccine due next dose number^LN|8|1||||||F
OBX|48|TS|30981-5^Earliest date to give^LN|8|20180101||||||F
OBX|49|CE|30982-3^Reason applied by forecast logic to project this vaccine^LN|8|ACIP schedule||||||F";

    [Fact]
    public void Parse_Real_CAIR2_RSP_Message_Test()
    {
        // Arrange
        var parser = new Hl7Parser();

        // Act
        var message = parser.ParseMessage(RealCAIR2Message);

        // Assert
        Assert.NotNull(message);
        Assert.True(message.Segments.Count > 10, "Message should have multiple segments");
    }

    [Fact]
    public void Extract_Patient_From_Real_Message_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);

        // Act
        var pid = message.GetSegment<PIDSegment>("PID");

        // Assert
        Assert.NotNull(pid);
        Assert.Equal("291235^^^ORA^SR", pid.PatientIdentifierList);
        Assert.Equal("WALL^MIKE", pid.PatientName);
        Assert.Equal("20170101", pid.DateOfBirth);
        Assert.Equal("M", pid.AdministrativeSex);
        Assert.Equal("2222 ANYWHERE WAY^^FRESNO^CA^93726^^H", pid.PatientAddress);
    }

    [Fact]
    public void Extract_MSH_From_Real_Message_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);

        // Act
        var msh = message.GetSegment<MSHSegment>("MSH");

        // Assert
        Assert.NotNull(msh);
        Assert.Equal("CAIR IIS", msh.SendingApplication);
        Assert.Equal("CAIR IIS", msh.SendingFacility);
        Assert.Equal("DE000001", msh.ReceivingFacility);
        Assert.Equal("RSP^K11^RSP_K11", msh.MessageType);
        Assert.Equal("200", msh.MessageControlId);
        Assert.Equal("2.5.1", msh.VersionId);
    }

    [Fact]
    public void Count_RXA_Segments_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);

        // Act
        var rxaSegments = message.GetSegments<RXASegment>("RXA");

        // Assert
        Assert.NotNull(rxaSegments);
        Assert.Equal(3, rxaSegments.Count); // 3 RXA records: HepB, DTaP, No Vaccine
    }

    [Fact]
    public void Extract_Administered_Vaccines_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);

        // Act
        var rxaSegments = message.GetSegments<RXASegment>("RXA");

        // Assert
        Assert.Equal(3, rxaSegments.Count);

        // First vaccine: HepB
        Assert.Equal("20170101", rxaSegments[0].DateTimeOfAdministration);
        Assert.Equal("08^HepBPeds^CVX", rxaSegments[0].AdministeredCode);
        Assert.Equal("1.0", rxaSegments[0].AdministeredAmount);
        Assert.Equal("HBV12345", rxaSegments[0].SubstanceLotNumber);
        Assert.Equal("CP", rxaSegments[0].CompletionStatus);

        // Second vaccine: DTaP
        Assert.Equal("20170301", rxaSegments[1].DateTimeOfAdministration);
        Assert.Equal("20^DTaP^CVX", rxaSegments[1].AdministeredCode);
        Assert.Equal("CP", rxaSegments[1].CompletionStatus);

        // Third: No vaccine administered
        Assert.Equal("20170509", rxaSegments[2].DateTimeOfAdministration);
        Assert.Equal("998^No Vaccine Administered^CVX", rxaSegments[2].AdministeredCode);
    }

    [Fact]
    public void Count_OBX_Segments_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);

        // Act
        var obxSegments = message.GetSegments<OBXSegment>("OBX");

        // Assert
        Assert.NotNull(obxSegments);
        Assert.Equal(49, obxSegments.Count); // 49 OBX records for vaccine forecasting
    }

    [Fact]
    public void Extract_First_OBX_Observation_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);

        // Act
        var obxSegments = message.GetSegments<OBXSegment>("OBX");

        // Assert
        Assert.NotNull(obxSegments);
        Assert.True(obxSegments.Count > 0);

        var firstObx = obxSegments[0];
        Assert.Equal(1, firstObx.SetId);
        Assert.Equal("CE", firstObx.ValueType);
        Assert.Equal("38890-0^COMPONENT VACCINE TYPE^LN", firstObx.ObservationIdentifier);
        Assert.Equal("45^HepB^CVX^90731^HepB^CPT", firstObx.ObservationValue);
    }

    [Fact]
    public void Validate_Real_Message_Structure_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);
        var validator = new Hl7MessageValidator();

        // Act
        var result = validator.Validate(message);

        // Assert
        Assert.NotNull(result);
        // RSP messages may have warnings about certain fields, but should be valid overall
        Assert.True(result.Errors.Count == 0 || result.IsValid, 
            $"Validation errors: {string.Join(", ", result.Errors)}");
    }

    [Fact]
    public void Extract_Patient_Demographics_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);

        // Act
        var pid = message.GetSegment<PIDSegment>("PID");

        // Assert
        Assert.NotNull(pid);
        
        // Verify all patient fields
        Console.WriteLine("=== PATIENT DEMOGRAPHICS ===");
        Console.WriteLine($"Patient ID: {pid.PatientId}");
        Console.WriteLine($"Name: {pid.PatientName}");
        Console.WriteLine($"DOB: {pid.DateOfBirth}");
        Console.WriteLine($"Sex: {pid.AdministrativeSex}");
        Console.WriteLine($"Address: {pid.PatientAddress}");
        Console.WriteLine($"Phone: {pid.PhoneNumberHome}");

        Assert.False(string.IsNullOrEmpty(pid.PatientIdentifierList));
        Assert.False(string.IsNullOrEmpty(pid.PatientName));
        Assert.False(string.IsNullOrEmpty(pid.DateOfBirth));
    }

    [Fact]
    public void Display_Administered_Vaccinations_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);

        // Act
        var rxaSegments = message.GetSegments<RXASegment>("RXA");

        // Assert & Display
        Console.WriteLine("\n=== ADMINISTERED VACCINATIONS ===");
        foreach (var rxa in rxaSegments)
        {
            Console.WriteLine($"Date: {rxa.DateTimeOfAdministration}");
            Console.WriteLine($"Vaccine: {rxa.AdministeredCode}");
            Console.WriteLine($"Amount: {rxa.AdministeredAmount} {rxa.AdministeredUnits}");
            Console.WriteLine($"Lot Number: {rxa.SubstanceLotNumber}");
            Console.WriteLine($"Route: {rxa.AdministrationRoute}");
            Console.WriteLine($"Site: {rxa.AdministrationSite}");
            Console.WriteLine($"Status: {rxa.CompletionStatus}");
            Console.WriteLine("---");
        }

        Assert.True(rxaSegments.Count > 0);
    }

    [Fact]
    public void Parse_Vaccine_Forecast_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);

        // Act
        var obxSegments = message.GetSegments<OBXSegment>("OBX");

        // Assert & Display - Show vaccine forecasts
        Console.WriteLine("\n=== VACCINE FORECAST ===");
        var vaccineForecasts = obxSegments.Where(o => o.ObservationIdentifier.Contains("30979-9")).ToList();
        
        foreach (var forecast in vaccineForecasts)
        {
            Console.WriteLine($"Vaccine Due: {forecast.ObservationValue}");
            
            // Find related forecast details
            var setId = forecast.SetId;
            var dueDate = obxSegments.FirstOrDefault(o => o.SetId == setId && o.ObservationIdentifier.Contains("30980-7"));
            var doseNumber = obxSegments.FirstOrDefault(o => o.SetId == setId && o.ObservationIdentifier.Contains("30973-2"));
            var earliestDate = obxSegments.FirstOrDefault(o => o.SetId == setId && o.ObservationIdentifier.Contains("30981-5"));
            
            if (dueDate != null)
                Console.WriteLine($"  Due Date: {dueDate.ObservationValue}");
            if (doseNumber != null)
                Console.WriteLine($"  Dose #: {doseNumber.ObservationValue}");
            if (earliestDate != null)
                Console.WriteLine($"  Earliest: {earliestDate.ObservationValue}");
            Console.WriteLine();
        }

        Assert.True(vaccineForecasts.Count > 0);
    }

    [Fact]
    public void Message_Has_Correct_Segment_Count_Test()
    {
        // Arrange
        var parser = new Hl7Parser();

        // Act
        var message = parser.ParseMessage(RealCAIR2Message);

        // Assert
        var segmentCounts = new Dictionary<string, int>();
        foreach (var segment in message.Segments)
        {
            if (!segmentCounts.ContainsKey(segment.SegmentId))
                segmentCounts[segment.SegmentId] = 0;
            segmentCounts[segment.SegmentId]++;
        }

        Console.WriteLine("\n=== SEGMENT COUNT ===");
        foreach (var kvp in segmentCounts.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        // Verify expected segments
        Assert.True(segmentCounts.ContainsKey("MSH"), "Should have MSH segment");
        Assert.True(segmentCounts.ContainsKey("MSA"), "Should have MSA segment");
        Assert.True(segmentCounts.ContainsKey("PID"), "Should have PID segment");
        Assert.True(segmentCounts.ContainsKey("RXA"), "Should have RXA segments");
        Assert.True(segmentCounts.ContainsKey("OBX"), "Should have OBX segments");
    }

    [Fact]
    public void Serialize_And_Reparse_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var originalMessage = parser.ParseMessage(RealCAIR2Message);
        var msh = originalMessage.GetSegment<MSHSegment>("MSH");
        var separators = new Hl7.Core.Utils.Hl7Separators();
        var serializer = new Hl7.Core.Hl7MessageSerializer(separators);

        // Act - Serialize first segment
        var serializedMsh = serializer.SerializeSegment(msh);

        // Assert
        Assert.NotNull(serializedMsh);
        Assert.StartsWith("MSH|", serializedMsh);
        Assert.Contains("CAIR IIS", serializedMsh);
        
        Console.WriteLine($"\n=== SERIALIZED MSH ===\n{serializedMsh}");
    }

    [Fact]
    public void Extract_Message_Metadata_Test()
    {
        // Arrange
        var cair2Parser = new CAIR2Parser();
        var message = cair2Parser.ParseVaccinationMessage(RealCAIR2Message);

        // Act
        var metadata = cair2Parser.ExtractMessageMetadata(message);

        // Assert & Display
        Assert.NotNull(metadata);
        
        Console.WriteLine("\n=== MESSAGE METADATA ===");
        Console.WriteLine($"Sending App: {metadata.SendingApplication}");
        Console.WriteLine($"Sending Facility: {metadata.SendingFacility}");
        Console.WriteLine($"Receiving App: {metadata.ReceivingApplication}");
        Console.WriteLine($"Receiving Facility: {metadata.ReceivingFacility}");
        Console.WriteLine($"Message Type: {metadata.MessageType}");
        Console.WriteLine($"Message ID: {metadata.MessageControlId}");
        Console.WriteLine($"DateTime: {metadata.MessageDateTime}");
        Console.WriteLine($"Version: {metadata.VersionId}");
        Console.WriteLine($"Processing ID: {metadata.ProcessingId}");

        Assert.Equal("CAIR IIS", metadata.SendingApplication);
        Assert.Equal("RSP^K11^RSP_K11", metadata.MessageType);
    }

    [Fact]
    public void Message_Contains_All_Required_Elements_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var message = parser.ParseMessage(RealCAIR2Message);

        // Act & Assert
        var msh = message.GetSegment<MSHSegment>("MSH");
        var pid = message.GetSegment<PIDSegment>("PID");
        var rxa = message.GetSegment<RXASegment>("RXA");
        var obx = message.GetSegment<OBXSegment>("OBX");

        Assert.NotNull(msh);
        Assert.NotNull(pid);
        Assert.NotNull(rxa);
        Assert.NotNull(obx);

        // All core segments present
        Console.WriteLine("\n=== MESSAGE STRUCTURE VALIDATION ===");
        Console.WriteLine($"✓ MSH Present: {msh != null}");
        Console.WriteLine($"✓ MSA Present: {message.GetSegment<Segment>("MSA") != null}");
        Console.WriteLine($"✓ QAK Present: {message.GetSegment<Segment>("QAK") != null}");
        Console.WriteLine($"✓ QPD Present: {message.GetSegment<Segment>("QPD") != null}");
        Console.WriteLine($"✓ PID Present: {pid != null}");
        Console.WriteLine($"✓ RXA Present: {rxa != null}");
        Console.WriteLine($"✓ OBX Present: {obx != null}");
    }
}
