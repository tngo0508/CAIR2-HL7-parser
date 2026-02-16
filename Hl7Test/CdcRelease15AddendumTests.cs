using Hl7.Core;
using Hl7.Core.Segments;
using Hl7.Core.Types;
using Xunit;

namespace Hl7Test;

public class CdcRelease15AddendumTests
{
    private readonly Hl7Parser _parser = new Hl7Parser();

    [Fact]
    public void Parse_VXU_CdcRelease15Addendum_Test()
    {
        // Sample VXU based on Release 1.5 + Addendum (2018)
        // Correct RXA field mapping:
        // 1:0, 2:1, 3:201810151000, 4:201810151000, 5:90658^Influenza^CPT, 6:0.5, 7:mL^milliliters^UCUM, 8:(DosageForm), 
        // 9:00^New immunization^HL70322, 10:^LEFT ARM, 11:IM^Intramuscular^HL70162, 12:(NotUsed), 13:(NotUsed), 14:(NotUsed),
        // 15:LOT123, 16:20201231, 17:MSD^Merck^MVX, 18:(Refusal), 19:(Indication), 20:CP^Complete^HL70322, 21:A, 22:201810151005
        const string vxu = @"MSH|^~\&|MYAPP|MYFAC|IISAPP|IISFAC|201810151000||VXU^V04^VXU_V04|MSG001|P|2.5.1|||ER|AL|||||Z22^CDCPHINVS
PID|1||MRN123^^^MYFAC^MR||DOE^JOHN^J^JR||20180101|M||2106-3^White^HL70005|123 MAIN ST^^FRESNO^CA^93701^^H||^PRN^PH^^^555^1234567||eng^English^HL70296||||||||N|0
ORC|RE||12345^MYAPP|||||||||1234567890^PROVIDER^JOE^^^^^^NPI
RXA|0|1|201810151000|201810151000|90658^Influenza^CPT|0.5|mL^milliliters^UCUM||00^New immunization^HL70322|^LEFT ARM|IM^Intramuscular^HL70162||||LOT123|20201231|MSD^Merck^MVX|||CP^Complete^HL70322|A|201810151005
OBX|1|CE|64994-7^Vaccine funding program eligibility category^LN|1|V02^VFC eligible - Medicaid/Medicaid Number^HL70064||||||F
ERR||PID^1^3^1^5^1|101^Required field missing^HL70357|E";

        // Act
        var message = _parser.ParseMessage(vxu);

        // Assert
        Assert.NotNull(message);
        
        // MSH
        var msh = message.GetSegment<MSHSegment>("MSH");
        Assert.Equal("Z22^CDCPHINVS", msh.MessageProfileIdentifier);

        // RXA-22 (System Entry Date/Time - TSU)
        var rxa = message.GetSegment<RXASegment>("RXA");
        Assert.Equal("201810151005", rxa.SystemEntryDateTime.Time);

        // ERR-2 (Error Location - ERN)
        var err = message.GetSegment<ERRSegment>("ERR");
        Assert.NotNull(err);
        Assert.Equal("PID", err.ErrorLocation.SegmentId);
        Assert.Equal("1", err.ErrorLocation.Sequence);
        Assert.Equal("3", err.ErrorLocation.FieldPosition);
        Assert.Equal("1", err.ErrorLocation.FieldRepetition);
        Assert.Equal("5", err.ErrorLocation.ComponentNumber);

        // ERR-3 (HL7 Error Code - CE)
        Assert.Equal("101", err.Hl7ErrorCode.Identifier);
        Assert.Equal("Required field missing", err.Hl7ErrorCode.Text);
        Assert.Equal("HL70357", err.Hl7ErrorCode.NameOfCodingSystem);
    }

    [Fact]
    public void Parse_Batch_Release15Addendum_Test()
    {
        const string batch = @"FHS|^~\&|SENDAPP|SENDFAC|RECAPP|RECFAC|201810151000
BHS|^~\&|SENDAPP|SENDFAC|RECAPP|RECFAC|201810151000
MSH|^~\&|MYAPP|MYFAC|IISAPP|IISFAC|201810151000||VXU^V04^VXU_V04|MSG001|P|2.5.1
BTS|1
FTS|1";

        // Act
        var message = _parser.ParseMessage(batch);

        // Assert
        Assert.NotNull(message.GetSegment<FHSSegment>("FHS"));
        Assert.NotNull(message.GetSegment<BHSSegment>("BHS"));
        Assert.NotNull(message.GetSegment<BTSSegment>("BTS"));
        Assert.NotNull(message.GetSegment<FTSSegment>("FTS"));
        
        var fhs = message.GetSegment<FHSSegment>("FHS");
        Assert.Equal("SENDAPP", fhs.FileSendingApplication.NamespaceId);
        Assert.Equal("201810151000", fhs.FileCreationDateTime.Time);
    }
}
