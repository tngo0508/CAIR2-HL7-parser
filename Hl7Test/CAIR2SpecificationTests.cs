using Hl7.Core;
using Hl7.Core.CAIR2;
using Hl7.Core.Segments;
using Hl7.Core.Base;
using Hl7.Core.Common;

namespace Hl7Test;

/// <summary>
/// Test cases based on the CAIR2 HL7 v2.5.1 Data Exchange Specification samples.
/// Document: https://www.cdph.ca.gov/Programs/CID/DCDC/CAIR/CDPH%20Document%20Library/CAIR2_HL7v2.5.1DataExchangeSpecification.pdf
/// </summary>
public class CAIR2SpecificationTests
{
    private readonly Hl7Parser _parser = new Hl7Parser();

    /// <summary>
    /// VXU Message Sample from CAIR2 Specification (approximate based on common patterns in the document)
    /// </summary>
    private const string VXUSample = @"MSH|^~\&|MyEMR|DE-000001||CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1|||ER|AL|||||Z22^CDCPHINVS|DE-000001
PID|1||123456789^^^MR||DOE^JOHN^J^JR||19800101|M||2106-3^White^HL70005|123 MAIN ST^^FRESNO^CA^93701^^H||^PRN^PH^^^555^1234567||eng^English^HL70296||||||||N|0
PD1|||||||||||02^No Recall - reminder^HL70435|N
NK1|1|DOE^JANE|MTH^Mother^HL70063|123 MAIN ST^^FRESNO^CA^93701
ORC|RE||123456^EMR|||||||||1234567890^Provider^Joe^^^^^^NPI
RXA|0|1|20160701120000|20160701120000|90658^Influenza^CPT|0.5|mL^milliliters^UCUM||00^New immunization^HL70322|^LEFT ARM|IM^Intramuscular^HL70162|ABC12345|20180630|MSD^Merck^MVX|||||CP^Complete^HL70322|A
RXR|IM^Intramuscular^HL70162|LA^Left Arm^HL70163
OBX|1|CE|30956-7^Vaccine adverse event reported^LN|1|N^No^HL70136||||||F
OBX|2|CE|64994-7^Vaccine funding program eligibility^LN|1|V02^VFC eligible - Medicaid/Medicaid Number^HL70064||||||F";

    /// <summary>
    /// ACK Message Sample from CAIR2 Specification
    /// </summary>
    private const string ACKSample = @"MSH|^~\&|CAIR2|DE-000001|MyEMR|DE-000001|201607011230310700||ACK^V04^ACK|CA0001_ACK|P|2.5.1
MSA|AA|CA0001";

    /// <summary>
    /// QBP Message Sample from CAIR2 Specification (Z34 profile)
    /// </summary>
    private const string QBPSample = @"MSH|^~\&|MyEMR|DE-000001|CAIR2|CAIR2|201607011230300700||QBP^Q11^QBP_Q11|CA0002|P|2.5.1|||ER|AL|||||Z34^CDCPHINVS
QPD|Z34^Request Immunization History^CDCPHINVS|CA0002|123456789^^^MR|DOE^JOHN^J^JR|DOE^JANE|19800101|M|123 MAIN ST^^FRESNO^CA^93701^^H|^PRN^PH^^^555^1234567
RCP|I|10^RD|T";

    /// <summary>
    /// RSP Message Sample from CAIR2 Specification (Z32 profile - Exact Match)
    /// </summary>
    private const string RSPSample = @"MSH|^~\&|CAIR2|CAIR2|MyEMR|DE-000001|201607011230320700||RSP^K11^RSP_K11|CA0002|P|2.5.1|||||||||Z32^CDCPHINVS
MSA|AA|CA0002
QAK|CA0002|OK|Z34^Request Immunization History^CDCPHINVS
QPD|Z34^Request Immunization History^CDCPHINVS|CA0002|123456789^^^MR|DOE^JOHN^J^JR|DOE^JANE|19800101|M|123 MAIN ST^^FRESNO^CA^93701^^H|^PRN^PH^^^555^1234567
PID|1||123456789^^^MR||DOE^JOHN^J^JR||19800101|M||2106-3^White^HL70005|123 MAIN ST^^FRESNO^CA^93701^^H||^PRN^PH^^^555^1234567||eng^English^HL70296||||||||N|0
ORC|RE||123456^EMR|||||||||1234567890^Provider^Joe^^^^^^NPI
RXA|0|1|20160701120000|20160701120000|90658^Influenza^CPT|0.5|mL^milliliters^UCUM||00^New immunization^HL70322|^LEFT ARM|IM^Intramuscular^HL70162|ABC12345|20180630|MSD^Merck^MVX|||||CP^Complete^HL70322|A";

    private const string VXUSample2 = @"MSH|^~\&|MyEMR|DE000001||CAIR2|20200225123030||VXU^V04^VXU_V04|CA0001|P|2.5.1|||AL|AL||||||DE-000001
PID|1||PA123456^^^XYZCLINIC^MR||JONES^GEORGE^M^JR|MILLER^MARTHA^G|20140227|M||2106-3^WHITE^HL70005|1234 W FIRST ST^^BEVERLY HILLS^CA^90210^^H||^PRN^PH^^^555^5555555~^PRN^CP^^^555^5551234~^NET^X.400^cair@cair.com||ENG^English^HL70296|||||||2186-5^not Hispanic or Latino^HL70189||Y|2
PD1|||||||||||02^REMINDER/RECALL – ANY METHOD^HL70215|N|20140730|||A|20140730|
NK1|1|JONES^MARTHA|MTH^MOTHER^HL70063||||||||||||||
ORC|RE||197023^CMC|||||||^Clark^Dave||1245319599^Smith^Janet^^^^^^CMS_NPPES^^^^NPI^^^^^^^MD|||||
RXA|0|1|20200225||08^HEPB-PEDIATRIC/ADOLESCENT^CVX|.5|mL^mL^UCUM||00^NEW IMMUNIZATION RECORD^NIP001|85041235^Bear^Elizabeth^^^^^^NG^^^^NP^^^^^^^^NP|^^^DE000001||||0039F|20200531|MSD^MERCK^MVX|||CP|A
RXR|IM^INTRAMUSCULAR^HL70162|LA^LEFT ARM^HL70163
OBX|1|CE|64994-7^Vaccine funding program eligibility category^LN|1|V03^VFC eligibility – Uninsured^HL70064||||||F|||20200225140500";

    [Fact]
    public void Parse_VXU_Sample2_Test()
    {
        // Act
        var message = _parser.ParseMessage(VXUSample2);

        // Assert
        Assert.NotNull(message);
        var msh = message.GetSegment<MSHSegment>("MSH");
        Assert.Equal("VXU^V04^VXU_V04", msh.MessageType);
        Assert.Equal("DE000001", msh.SendingFacility);

        var pid = message.GetSegment<PIDSegment>("PID");
        Assert.NotNull(pid);
        Assert.Equal("JONES^GEORGE^M^JR", pid.PatientName);
        Assert.Equal("20140227", pid.DateOfBirth);
        Assert.Equal("2106-3^WHITE^HL70005", pid.Race);

        var pd1 = message.GetSegment<PD1Segment>("PD1");
        Assert.NotNull(pd1);
        Assert.Equal("N", pd1.ProtectionIndicator);

        var nk1 = message.GetSegment<NK1Segment>("NK1");
        Assert.NotNull(nk1);
        Assert.Equal("JONES^MARTHA", nk1.Name);
        Assert.Equal("MTH^MOTHER^HL70063", nk1.Relationship);

        var rxa = message.GetSegment<RXASegment>("RXA");
        Assert.NotNull(rxa);
        Assert.Equal("08^HEPB-PEDIATRIC/ADOLESCENT^CVX", rxa.AdministeredCode);
        Assert.Equal(".5", rxa.AdministeredAmount);
        Assert.Equal("0039F", rxa.SubstanceLotNumber);

        var rxr = message.GetSegment<RXRSegment>("RXR");
        Assert.NotNull(rxr);
        Assert.Equal("IM^INTRAMUSCULAR^HL70162", rxr.Route);
        Assert.Equal("LA^LEFT ARM^HL70163", rxr.AdministrationSite);

        var obx = message.GetSegment<OBXSegment>("OBX");
        Assert.NotNull(obx);
        Assert.Equal("64994-7^Vaccine funding program eligibility category^LN", obx.ObservationIdentifier);
        Assert.Equal("V03^VFC eligibility – Uninsured^HL70064", obx.ObservationValue);
    }

    [Fact]
    public void Parse_VXU_Specification_Sample_Test()
    {
        // Act
        var message = _parser.ParseMessage(VXUSample);

        // Assert
        Assert.NotNull(message);
        var msh = message.GetSegment<MSHSegment>("MSH");
        Assert.Equal("VXU^V04^VXU_V04", msh.MessageType);
        Assert.Equal("2.5.1", msh.VersionId);

        var pid = message.GetSegment<PIDSegment>("PID");
        Assert.NotNull(pid);
        Assert.Equal("DOE^JOHN^J^JR", pid.PatientName);
        Assert.Equal("19800101", pid.DateOfBirth);

        var rxa = message.GetSegment<RXASegment>("RXA");
        Assert.NotNull(rxa);
        Assert.Equal("90658^Influenza^CPT", rxa.AdministeredCode);
        Assert.Equal("ABC12345", rxa.SubstanceLotNumber);
    }

    [Fact]
    public void Parse_ACK_Specification_Sample_Test()
    {
        // Act
        var message = _parser.ParseMessage(ACKSample);

        // Assert
        Assert.NotNull(message);
        var msh = message.GetSegment<MSHSegment>("MSH");
        Assert.Equal("ACK^V04^ACK", msh.MessageType);

        var msa = message.GetSegment<Segment>("MSA");
        Assert.NotNull(msa);
        Assert.Equal("AA", msa.GetField(1));
        Assert.Equal("CA0001", msa.GetField(2));
    }

    [Fact]
    public void Parse_QBP_Specification_Sample_Test()
    {
        // Act
        var message = _parser.ParseMessage(QBPSample);

        // Assert
        Assert.NotNull(message);
        var msh = message.GetSegment<MSHSegment>("MSH");
        Assert.Equal("QBP^Q11^QBP_Q11", msh.MessageType);
        Assert.Equal("Z34^CDCPHINVS", msh.MessageProfileIdentifier);

        var qpd = message.GetSegment<QPDSegment>("QPD");
        Assert.NotNull(qpd);
        Assert.Equal("Z34^Request Immunization History^CDCPHINVS", qpd.MessageQueryName);
        Assert.Equal("CA0002", qpd.QueryTag);
        Assert.Equal("123456789^^^MR", qpd.PatientIdentifierList);
        Assert.Equal("DOE^JOHN^J^JR", qpd.PatientName);
        Assert.Equal("DOE^JANE", qpd.MothersMaidenName);
        Assert.Equal("19800101", qpd.DateOfBirth);
        Assert.Equal("M", qpd.AdministrativeSex);
        Assert.Equal("123 MAIN ST^^FRESNO^CA^93701^^H", qpd.PatientAddress);
        Assert.Equal("^PRN^PH^^^555^1234567", qpd.PhoneNumberHome);
    }

    [Fact]
    public void Parse_RSP_Specification_Sample_Test()
    {
        // Act
        var message = _parser.ParseMessage(RSPSample);

        // Assert
        Assert.NotNull(message);
        var msh = message.GetSegment<MSHSegment>("MSH");
        Assert.Equal("RSP^K11^RSP_K11", msh.MessageType);

        var msa = message.GetSegment<Segment>("MSA");
        Assert.Equal("AA", msa.GetField(1));

        var qak = message.GetSegment<QAKSegment>("QAK");
        Assert.NotNull(qak);
        Assert.Equal("CA0002", qak.QueryTag);
        Assert.Equal("OK", qak.QueryResponseStatus);

        var pid = message.GetSegment<PIDSegment>("PID");
        Assert.NotNull(pid);
        Assert.Equal("DOE^JOHN^J^JR", pid.PatientName);
    }
}
