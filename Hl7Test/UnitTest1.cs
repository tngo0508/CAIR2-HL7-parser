using Hl7.Core;
using Hl7.Core.CAIR2;
using Hl7.Core.Segments;
using Hl7.Core.Validation;
using Hl7.Core.Utils;
using Hl7.Core.Base;
using Hl7.Core.Common;

namespace Hl7Test;

public class UnitTest1
{
    [Fact]
    public void MSH_Segment_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        // Act
        var mshSegment = parser.ParseMSHSegment("MSH|^~\\&|MyEMR|DE-000001| |CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1|||ER|AL|||||Z22^CDCPHINVS|DE-000001 ");
        // Assert
        Assert.NotNull(mshSegment);
        Assert.Equal("MyEMR", mshSegment.SendingApplication);
        Assert.Equal("CAIR2", mshSegment.ReceivingFacility);
        Assert.Equal("2.5.1", mshSegment.VersionId);
    }

    [Fact]
    public void Parse_PID_Segment_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var pidLine = "PID|1|123456789|987654321||DOE^JOHN||19800101|M|";

        // Act
        var pidSegment = parser.ParseSegment(pidLine) as PIDSegment;

        // Assert
        Assert.NotNull(pidSegment);
        Assert.Equal("PID", pidSegment.SegmentId);
        Assert.Equal(1, pidSegment.SetId);
        Assert.Equal("123456789", pidSegment.PatientId);
        Assert.Equal("DOE^JOHN", pidSegment.PatientName);
        Assert.Equal("19800101", pidSegment.DateOfBirth);
        Assert.Equal("M", pidSegment.AdministrativeSex);
    }

    [Fact]
    public void Parse_RXA_Segment_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var rxaLine = "RXA|0|1|20160701120000||90658^INFLUENZA VACCINE|0.5|mL||00|^LEFT ARM|IM|ABC123|20180630||MERCK||||||A";

        // Act
        var rxaSegment = parser.ParseSegment(rxaLine) as RXASegment;

        // Assert
        Assert.NotNull(rxaSegment);
        Assert.Equal("RXA", rxaSegment.SegmentId);
        Assert.Equal(0, rxaSegment.GiveSubIdCounter);
        Assert.Equal("1", rxaSegment.AdministrationSubIdCounter);
        Assert.Equal("20160701120000", rxaSegment.DateTimeOfAdministration);
        Assert.Equal("90658^INFLUENZA VACCINE", rxaSegment.AdministeredCode);
        Assert.Equal("0.5", rxaSegment.AdministeredAmount);
        Assert.Equal("ABC123", rxaSegment.SubstanceLotNumber);
    }

    [Fact]
    public void Parse_OBX_Segment_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var obxLine = "OBX|1|CE|30956-7^VACCINE ADVERSE EVENT REPORTED|1|V01^^L|||||F";

        // Act
        var obxSegment = parser.ParseSegment(obxLine) as OBXSegment;

        // Assert
        Assert.NotNull(obxSegment);
        Assert.Equal("OBX", obxSegment.SegmentId);
        Assert.Equal(1, obxSegment.SetId);
        Assert.Equal("CE", obxSegment.ValueType);
        Assert.Equal("30956-7^VACCINE ADVERSE EVENT REPORTED", obxSegment.ObservationIdentifier);
    }

    [Fact]
    public void CAIR2_Extract_Vaccination_Records_Test()
    {
        // Arrange
        var cair2Parser = new CAIR2Parser();
        var hl7Message = @"MSH|^~\&|MyEMR|DE-000001||CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1
PID|1|123456789|987654321||DOE^JOHN||19800101|M|
RXA|0|1|20160701120000||90658^INFLUENZA VACCINE|0.5|mL||00|^LEFT ARM|IM|ABC123|20180630||MERCK||||||A
RXA|0|2|20150701120000||107^DTAP|0.5|mL||00|^RIGHT ARM|IM|XYZ789|20180630||PFIZER||||||A";

        // Act
        var message = cair2Parser.ParseVaccinationMessage(hl7Message);
        var records = cair2Parser.ExtractVaccinationRecords(message);

        // Assert
        Assert.NotNull(message);
        Assert.Equal(2, records.Count);
        Assert.Equal("123456789", records[0].PatientId);
        Assert.Equal("DOE^JOHN", records[0].PatientName);
        Assert.Equal("90658^INFLUENZA VACCINE", records[0].VaccineCode);
        Assert.Equal("20160701120000", records[0].AdministrationDate);
        Assert.Equal("ABC123", records[0].LotNumber);
    }

    [Fact]
    public void Message_Validation_Test()
    {
        // Arrange
        var validator = new Hl7MessageValidator();
        var hl7Message = @"MSH|^~\&|MyEMR|DE-000001||CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1
PID|1|123456789|987654321||DOE^JOHN||19800101|M|
RXA|0|1|20160701120000||90658^INFLUENZA VACCINE|0.5|mL||00|^LEFT ARM|IM|ABC123|20180630||MERCK||||||A";

        var parser = new Hl7Parser();
        var message = parser.ParseMessage(hl7Message);

        // Act
        var result = validator.Validate(message);

        // Assert
        Assert.True(result.IsValid || result.Errors.Count == 0);
    }

    [Fact]
    public void Parse_Complete_Message_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        var hl7Message = @"MSH|^~\&|MyEMR|DE-000001||CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1
PID|1|123456789|987654321||DOE^JOHN||19800101|M|
RXA|0|1|20160701120000||90658^INFLUENZA VACCINE|0.5|mL||00|^LEFT ARM|IM|ABC123|20180630||MERCK||||||A";

        // Act
        var message = parser.ParseMessage(hl7Message);

        // Assert
        Assert.NotNull(message);
        Assert.Equal(3, message.Segments.Count);
        
        var msh = message.GetSegment<MSHSegment>("MSH");
        Assert.NotNull(msh);
        Assert.Equal("MyEMR", msh.SendingApplication);
        
        var pid = message.GetSegment<PIDSegment>("PID");
        Assert.NotNull(pid);
        Assert.Equal("123456789", pid.PatientId);
        
        var rxa = message.GetSegment<RXASegment>("RXA");
        Assert.NotNull(rxa);
        Assert.Equal("20160701120000", rxa.DateTimeOfAdministration);
    }

    [Fact]
    public void Message_Serialization_Test()
    {
        // Arrange
        var msh = new MSHSegment
        {
            SendingApplication = "TestApp",
            SendingFacility = "TestFac",
            ReceivingApplication = "ReceivingApp",
            ReceivingFacility = "ReceivingFac",
            MessageType = "VXU^V04^VXU_V04",
            MessageControlId = "MSG0001",
            ProcessingId = "P",
            VersionId = "2.5.1"
        };

        var message = new Hl7Message();
        message.AddSegment(msh);

        var separators = new Hl7Separators();
        var serializer = new Hl7MessageSerializer(separators);

        // Act
        var hl7String = serializer.SerializeSegment(msh);

        // Assert
        Assert.NotNull(hl7String);
        Assert.StartsWith("MSH|", hl7String);
        Assert.Contains("TestApp", hl7String);
    }

    [Fact]
    public void CAIR2_Extract_Patient_Info_Test()
    {
        // Arrange
        var cair2Parser = new CAIR2Parser();
        var hl7Message = @"MSH|^~\&|MyEMR|DE-000001||CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1
PID|1|123456789|987654321||DOE^JOHN||19800101|M|C|123 MAIN ST||555-1234||ENG|";

        // Act
        var message = cair2Parser.ParseVaccinationMessage(hl7Message);
        var patientInfo = cair2Parser.ExtractPatientInfo(message);

        // Assert
        Assert.NotNull(patientInfo);
        Assert.Equal("123456789", patientInfo.PatientId);
        Assert.Equal("DOE^JOHN", patientInfo.PatientName);
        Assert.Equal("19800101", patientInfo.DateOfBirth);
        Assert.Equal("M", patientInfo.AdministrativeSex);
    }

    [Fact]
    public void CAIR2_Extract_Message_Metadata_Test()
    {
        // Arrange
        var cair2Parser = new CAIR2Parser();
        var hl7Message = @"MSH|^~\&|MyEMR|DE-000001||CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1
PID|1|123456789|987654321||DOE^JOHN||19800101|M|";

        // Act
        var message = cair2Parser.ParseVaccinationMessage(hl7Message);
        var metadata = cair2Parser.ExtractMessageMetadata(message);

        // Assert
        Assert.NotNull(metadata);
        Assert.Equal("MyEMR", metadata.SendingApplication);
        Assert.Equal("CAIR2", metadata.ReceivingFacility);
        Assert.Equal("VXU^V04^VXU_V04", metadata.MessageType);
        Assert.Equal("2.5.1", metadata.VersionId);
    }

    [Fact]
    public void Parse_Composite_Field_Test()
    {
        // Arrange
        var separators = new Hl7Separators();
        var compositeValue = "90658^INFLUENZA VACCINE^2024";

        // Act
        var composite = Hl7FieldHelper.ParseComposite(compositeValue, separators);

        // Assert
        Assert.NotNull(composite);
        Assert.Equal(3, composite.Components.Count);
        Assert.Equal("90658", composite.GetComponent(0));
        Assert.Equal("INFLUENZA VACCINE", composite.GetComponent(1));
        Assert.Equal("2024", composite.GetComponent(2));
    }
}