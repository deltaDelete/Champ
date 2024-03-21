using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class MedCard {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long MedCardId { get; set; } = 0;

    [ForeignKey(nameof(Patient))]
    public long PatientId { get; set; }

    public Patient? Patient { get; set; }

    public DateTimeOffset DateOfIssue { get; set; } = DateTimeOffset.Now;

    [Column(TypeName = "MEDIUMBLOB")]
    public byte[]? Photo { get; set; }
}