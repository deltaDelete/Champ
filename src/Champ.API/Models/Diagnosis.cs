using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class Diagnosis {
    public long DiagnosisId { get; set; } = 0;

    [MaxLength(255)]
    [Column("Diagnosis")]
    public string Description { get; set; } = string.Empty;
}