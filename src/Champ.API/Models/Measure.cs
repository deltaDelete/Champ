using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Champ.API.Models;

public class Measure {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long MeasureId { get; set; } = 0;

    [ForeignKey(nameof(MedCard))]
    public long MedCardId { get; set; } = 0;

    public MedCard? MedCard { get; set; }

    [Column(TypeName = "TIMESTAMP")]
    public DateTimeOffset MeasureDate { get; set; } = DateTimeOffset.Now;

    [ForeignKey(nameof(Doctor))]
    public long DoctorId { get; set; }

    public Doctor? Doctor { get; set; }

    [ForeignKey(nameof(MeasureType))]
    public long MeasureTypeId { get; set; }

    public MeasureType? MeasureType { get; set; }

    [MaxLength(255)]
    public string MeasureName { get; set; } = string.Empty;

    [MaxLength(4096)]
    public string Results { get; set; } = string.Empty;

    [MaxLength(4096)]
    public string Recommendations { get; set; } = string.Empty;

    [Precision(10, 2)]
    public decimal Price { get; set; } = 0;
}
