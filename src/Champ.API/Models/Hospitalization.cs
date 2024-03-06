using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

namespace Champ.API.Models;

public class Hospitalization {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HospitalizationId { get; set; } = 0;

    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; } = 0;

    public Patient? Patient { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [DefaultValue("CURRENT_DATE")]
    public DateTimeOffset AdmissionDate { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [DefaultValue("CURRENT_DATE")]
    public DateTimeOffset DischargeDate { get; set; }

    [MaxLength(255)]
    public string Reason { get; set; } = string.Empty;

    [Column(TypeName = "json")]
    public string Additional { get; set; } = "{}";
}