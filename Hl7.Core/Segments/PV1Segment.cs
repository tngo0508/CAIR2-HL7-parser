using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;
using Hl7.Core.Types;

namespace Hl7.Core.Segments;

/// <summary>
/// PV1 - Patient Visit Segment
/// </summary>
[Segment("PV1")]
public class PV1Segment : Segment
{
    /// <summary>
    /// Gets or sets the Set ID - PV1 (PV1-1)
    /// </summary>
    [DataElement(1, "Set ID - PV1", ElementUsage.Optional)]
    public int SetId { get; set; }

    /// <summary>
    /// Gets or sets the Patient Class (PV1-2)
    /// </summary>
    [DataElement(2, "Patient Class", ElementUsage.Required)]
    public string PatientClass { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Assigned Patient Location (PV1-3)
    /// </summary>
    [DataElement(3, "Assigned Patient Location", ElementUsage.Optional)]
    public string AssignedPatientLocation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Admission Type (PV1-4)
    /// </summary>
    [DataElement(4, "Admission Type", ElementUsage.Optional)]
    public string AdmissionType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Attending Doctor (PV1-7)
    /// </summary>
    [DataElement(7, "Attending Doctor", ElementUsage.Optional)]
    public XPN AttendingDoctor { get; set; } = new();

    /// <summary>
    /// Gets or sets the Referring Doctor (PV1-8)
    /// </summary>
    [DataElement(8, "Referring Doctor", ElementUsage.Optional)]
    public XPN ReferringDoctor { get; set; } = new();

    /// <summary>
    /// Gets or sets the Consulting Doctor (PV1-9)
    /// </summary>
    [DataElement(9, "Consulting Doctor", ElementUsage.Optional)]
    public XPN ConsultingDoctor { get; set; } = new();

    /// <summary>
    /// Gets or sets the Hospital Service (PV1-10)
    /// </summary>
    [DataElement(10, "Hospital Service", ElementUsage.Optional)]
    public string HospitalService { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Admitting Doctor (PV1-17)
    /// </summary>
    [DataElement(17, "Admitting Doctor", ElementUsage.Optional)]
    public XPN AdmittingDoctor { get; set; } = new();

    /// <summary>
    /// Gets or sets the Visit Number (PV1-19)
    /// </summary>
    [DataElement(19, "Visit Number", ElementUsage.Optional)]
    public CX VisitNumber { get; set; } = new();

    /// <summary>
    /// Gets or sets the Financial Class (PV1-20)
    /// </summary>
    [DataElement(20, "Financial Class", ElementUsage.Optional)]
    public string FinancialClass { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Discharge Disposition (PV1-36)
    /// </summary>
    [DataElement(36, "Discharge Disposition", ElementUsage.Optional)]
    public string DischargeDisposition { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Admit Date/Time (PV1-44)
    /// </summary>
    [DataElement(44, "Admit Date/Time", ElementUsage.Optional)]
    public string AdmitDateTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Discharge Date/Time (PV1-45)
    /// </summary>
    [DataElement(45, "Discharge Date/Time", ElementUsage.Optional)]
    public string DischargeDateTime { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="PV1Segment"/> class
    /// </summary>
    public PV1Segment() : base("PV1")
    {
    }
}
