using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class Policy {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PolicyId { get; set; } = 0;

    public long PolicyNumber { get; set; }

    [MaxLength(255)]
    public string InsuranceCompany { get; set; } = string.Empty;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [DefaultValue("CURRENT_DATE")]
    public DateOnly ExpirationDate { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }

    public Patient? Patient { get; set; }
}