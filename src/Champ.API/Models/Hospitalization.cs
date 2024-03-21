using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

namespace Champ.API.Models;

public class Hospitalization {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long HospitalizationId { get; set; } = 0;

    [ForeignKey(nameof(MedCard))]
    public long MedCardId { get; set; } = 0;

    public MedCard? MedCard { get; set; }

    [ForeignKey(nameof(Diagnosis))]
    public long DiagosisId { get; set; } = 0;

    public Diagnosis? Diagnosis { get; set; }

    [MaxLength(4096)]
    public string Purpose { get; set; } = string.Empty;

    public bool IsPaid { get; set; }

    [Column(TypeName = "TIMESTAMP")]
    public DateTimeOffset DateStart { get; set; } = DateTimeOffset.Now;

    [Column(TypeName = "TIMESTAMP")]
    public DateTimeOffset DateEnd { get; set; } = DateTimeOffset.Now;

    [MaxLength(4096)]
    public string? AdditionalInfo { get; set; }

    public bool IsRejected { get; set; }

    [MaxLength(4096)]
    public string? RejectionReason { get; set; }

    [ForeignKey(nameof(Department))]
    public long DepartmentId { get; set; }

    public Department? Department { get; set; }
}
