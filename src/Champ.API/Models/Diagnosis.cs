using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class Diagnosis {
    public int DiagnosisId { get; set; } = 0;

    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }

    public Patient? Patient { get; set; }

    [MaxLength(255)]
    [Column("Diagnosis")]
    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string MedicalHistory { get; set; } = string.Empty;
}