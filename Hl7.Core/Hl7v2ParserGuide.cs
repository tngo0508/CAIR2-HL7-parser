namespace Hl7.Core;

/// <summary>
/// HL7v2 Parser for CAIR2 - Complete Usage Guide
/// 
/// This parser is designed to parse and process HL7v2 messages specifically for 
/// California's Immunization Registry (CAIR2) system.
/// 
/// HL7 Standard Message Structure:
/// - Segment: Delimited by '\r\n' or '\n'
/// - Field: Delimited by '|' (FieldSeparator from MSH segment)
/// - Component: Delimited by '^' (ComponentSeparator from MSH segment)
/// - Repetition: Delimited by '~' (RepetitionSeparator from MSH segment)
/// - Escape: Delimited by '\' (EscapeCharacter from MSH segment)
/// - SubComponent: Delimited by '&' (SubComponentSeparator from MSH segment)
/// 
/// MSH segment structure:
/// MSH|^~\&|SendingApp|SendingFac||ReceivingFac|DateTime||MessageType|ControlId|ProcessingId|Version
/// </summary>
public class Hl7v2ParserGuide
{
    /*
     * BASIC USAGE EXAMPLE:
     * 
     * using Hl7.Core;
     * using Hl7.Core.CAIR2;
     * 
     * // Example HL7 message for vaccination data
     * var hl7Message = @"MSH|^~\&|MyEMR|DE-000001||CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1
     * PID|1|123456789|987654321||DOE^JOHN||19800101|M|
     * RXA|0|1|20160701120000||90658^INFLUENZA VACCINE|0.5|mL||00|^LEFT ARM|IM|ABC123|20180630||MERCK||||||A";
     * 
     * // Parse the message
     * var parser = new Hl7Parser();
     * var message = parser.ParseMessage(hl7Message);
     * 
     * // Extract patient information
     * var pidSegment = message.GetSegment<PIDSegment>("PID");
     * Console.WriteLine($"Patient Name: {pidSegment.PatientName}");
     * Console.WriteLine($"Date of Birth: {pidSegment.DateOfBirth}");
     * 
     * // Extract vaccination records
     * var rxaSegments = message.GetSegments<RXASegment>("RXA");
     * foreach (var rxa in rxaSegments)
     * {
     *     Console.WriteLine($"Vaccine: {rxa.AdministeredCode}");
     *     Console.WriteLine($"Date: {rxa.DateTimeOfAdministration}");
     *     Console.WriteLine($"Administered At: {rxa.AdministeredAtLocation}");
     * }
     * 
     * // Use CAIR2-specific parser
     * var cair2Parser = new CAIR2Parser();
     * var vaccinationRecords = cair2Parser.ExtractVaccinationRecords(message);
     * foreach (var record in vaccinationRecords)
     * {
     *     Console.WriteLine($"Patient {record.PatientId} received {record.VaccineCode}");
     * }
     */

    /*
     * PARSING INDIVIDUAL SEGMENTS:
     * 
     * var parser = new Hl7Parser();
     * 
     * // Parse MSH segment (must be done first to extract separators)
     * var mshSegment = parser.ParseMSHSegment(
     *     "MSH|^~\\&|MyEMR|DE-000001||CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1");
     * 
     * // Parse other segments
     * var pidSegment = parser.ParseSegment("PID|1|123456789|987654321||DOE^JOHN||19800101|M|") as PIDSegment;
     * var rxaSegment = parser.ParseSegment("RXA|0|1|20160701120000||90658^INFLUENZA VACCINE|0.5|mL||00|^LEFT ARM|IM|ABC123|20180630||MERCK||||||A") as RXASegment;
     */

    /*
     * WORKING WITH COMPOSITE FIELDS:
     * 
     * using Hl7.Core.Utils;
     * using Hl7.Core.Types;
     * 
     * var separators = new Hl7Separators();
     * 
     * // Parse a composite field like "90658^INFLUENZA VACCINE"
     * var compositeValue = "90658^INFLUENZA VACCINE";
     * var composite = Hl7FieldHelper.ParseComposite(compositeValue, separators);
     * 
     * Console.WriteLine($"Component 1: {composite.GetComponent(0)}"); // 90658
     * Console.WriteLine($"Component 2: {composite.GetComponent(1)}"); // INFLUENZA VACCINE
     * 
     * // Work with repeating fields
     * var repeatingValue = "DOE^JOHN~SMITH^JANE";
     * var repeatingFields = Hl7FieldHelper.ParseRepeating(repeatingValue, separators);
     * foreach (var field in repeatingFields)
     * {
     *     Console.WriteLine(field);
     * }
     */

    /*
     * VALIDATING MESSAGES:
     * 
     * using Hl7.Core.Validation;
     * 
     * var validator = new Hl7MessageValidator();
     * var result = validator.Validate(message);
     * 
     * if (result.IsValid)
     * {
     *     Console.WriteLine("Message is valid!");
     * }
     * else
     * {
     *     foreach (var error in result.Errors)
     *     {
     *         Console.WriteLine($"Error: {error}");
     *     }
     * }
     * 
     * foreach (var warning in result.Warnings)
     * {
     *     Console.WriteLine($"Warning: {warning}");
     * }
     */

    /*
     * SERIALIZING MESSAGES (Converting back to HL7 format):
     * 
     * var msh = new MSHSegment
     * {
     *     SendingApplication = "MyEMR",
     *     ReceivingFacility = "CAIR2",
     *     VersionId = "2.5.1",
     *     MessageType = "VXU^V04^VXU_V04",
     *     MessageControlId = "CA0001"
     * };
     * 
     * var message = new Hl7Message();
     * message.AddSegment(msh);
     * 
     * var separators = new Hl7Separators();
     * var serializer = new Hl7MessageSerializer(separators);
     * var hl7String = serializer.Serialize(message);
     * 
     * Console.WriteLine(hl7String);
     */

    /*
     * CAIR2-SPECIFIC EXTRACTION:
     * 
     * var cair2Parser = new CAIR2Parser();
     * var hl7Message = // ... your HL7 message string
     * 
     * // Parse message
     * var message = cair2Parser.ParseVaccinationMessage(hl7Message);
     * 
     * // Validate CAIR2 message
     * bool isValid = cair2Parser.ValidateCAIR2Message(message);
     * 
     * // Extract structured data
     * var patientInfo = cair2Parser.ExtractPatientInfo(message);
     * var metadata = cair2Parser.ExtractMessageMetadata(message);
     * var vaccinations = cair2Parser.ExtractVaccinationRecords(message);
     * 
     * Console.WriteLine($"Patient: {patientInfo.PatientName}");
     * Console.WriteLine($"Message Type: {metadata.MessageType}");
     * Console.WriteLine($"Vaccinations: {vaccinations.Count}");
     */

    /*
     * HL7v2 SEGMENT REFERENCE FOR CAIR2:
     * 
     * MSH - Message Header
     *   Field 1: Field Separator (always |)
     *   Field 2: Encoding Characters (^~\&)
     *   Field 3: Sending Application
     *   Field 4: Sending Facility
     *   Field 5: Receiving Application
     *   Field 6: Receiving Facility
     *   Field 7: DateTime of Message
     *   Field 8: Security
     *   Field 9: Message Type
     *   Field 10: Message Control ID
     *   Field 11: Processing ID
     *   Field 12: Version ID
     * 
     * PID - Patient Identification
     *   Field 1: Set ID
     *   Field 2: Patient ID
     *   Field 3: Patient Identifier List
     *   Field 4: Alternate Patient ID
     *   Field 5: Patient Name (XPN^PN~PN...)
     *   Field 6: Mother's Maiden Name
     *   Field 7: Date of Birth
     *   Field 8: Administrative Sex (M/F/O/U)
     *   Field 10: Race
     *   Field 11: Patient Address
     *   Field 12: County Code
     *   Field 13: Phone Number - Home
     *   Field 14: Phone Number - Business
     *   Field 15: Primary Language
     * 
     * RXA - Pharmacy/Vaccine Administration
     *   Field 1: Give Sub-ID Counter
     *   Field 2: Administration Sub-ID Counter
     *   Field 3: Date/Time Start of Administration
     *   Field 4: Date/Time End of Administration
     *   Field 5: Administered Code
     *   Field 6: Administered Amount
     *   Field 7: Administered Units
     *   Field 9: Administration Notes
     *   Field 10: Administering Provider
     *   Field 11: Administered-at Location
     *   Field 12: Administered per (Time Unit)
     *   Field 13: Administered Strength
     *   Field 14: Administered Strength Units
     *   Field 15: Substance Lot Number
     *   Field 16: Substance Expiration Date
     *   Field 17: Substance Manufacturer Name
     *   Field 18: Substance Refusal Reason
     *   Field 19: Indication
     *   Field 20: Completion Status
     *   Field 21: Action Code
     * 
     * OBX - Observation/Result
     *   Field 1: Set ID
     *   Field 2: Value Type
     *   Field 3: Observation Identifier
     *   Field 4: Observation Sub-ID
     *   Field 5: Observation Value
     *   Field 6: Units
     *   Field 7: Reference Range
     *   Field 8: Abnormal Flags
     *   Field 9: Probability
     *   Field 10: Nature of Abnormal Test
     *   Field 11: Observation Result Status
     *   Field 12: Date/Time of Observation
     *   Field 13: Producer's Reference
     *   Field 14: Responsible Observer
     *   Field 15: Observation Method
     * 
     * OBR - Observation Request
     *   Field 1: Set ID
     *   Field 2: Placer Order Number
     *   Field 3: Filler Order Number
     *   Field 4: Universal Service Identifier
     *   Field 5: Priority
     *   Field 6: Requested Date/Time
     *   Field 7: Observation Date/Time
     *   Field 8: Observation End Date/Time
     *   Field 9: Collector's Comment
     *   Field 10: Orderer's Comments
     *   Field 11: Orderer's Name
     *   Field 12: Orderer's Address
     *   Field 13: Orderer's Phone Number
     *   Field 14: Orderer's Email Address
     */
}
