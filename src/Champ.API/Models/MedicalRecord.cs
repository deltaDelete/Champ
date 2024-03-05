using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class MedicalRecord {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MedicalRecordId { get; set; } = 0;

    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }

    public Patient? Patient { get; set; }

    [MaxLength(20)]
    public string MedicalCardNumber { get; set; } = string.Empty;
}