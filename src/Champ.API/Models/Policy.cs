using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Champ.API.Models;

public class Policy {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long PolicyId { get; set; } = 0;

    [ForeignKey(nameof(InsuranceCompany))]
    public long InsuranceCompanyId { get; set; } = 0;
    
    [JsonIgnore]
    public InsuranceCompany? InsuranceCompany { get; set; }

    [Column(TypeName = "TIMESTAMP")]
    public DateTimeOffset? ExpirationDate { get; set; } = DateTimeOffset.Now;

    [ForeignKey(nameof(Patient))]
    public long PatientId { get; set; } = 0;

    [JsonIgnore]
    public Patient? Patient { get; set; }
}