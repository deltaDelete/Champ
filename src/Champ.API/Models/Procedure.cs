using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class Procedure {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProcedureId { get; set; } = 0;

    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }

    public Patient? Patient { get; set; }

    public DateTimeOffset ProcedureDate { get; set; }

    [ForeignKey(nameof(Doctor))]
    public int DoctorId { get; set; }

    public Doctor? Doctor { get; set; }

    [ForeignKey(nameof(ProcedureType))]
    public int ProcedureTypeId { get; set; }

    public ProcedureType? ProcedureType { get; set; }

    [Column(TypeName = "text")]
    public string ProcedureName { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string Results { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string Recommendations { get; set; } = string.Empty;
}