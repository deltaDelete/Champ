using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class Policy {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long PolicyId { get; set; } = 0;

    [ForeignKey(nameof(InsuranceCompany))]
    public long InsuranceCompanyId { get; set; }
    public InsuranceCompany? InsuranceCompany { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [DefaultValue("CURRENT_TIMESTAMP")]
    [Column(TypeName = "TIMESTAMP")]
    public DateTimeOffset ExpirationDate { get; set; }

    [ForeignKey(nameof(Patient))]
    public long PatientId { get; set; }

    public Patient? Patient { get; set; }
}