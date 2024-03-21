using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class InsuranceCompany {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long InsuranceCompanyId { get; set; } = 0;

    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
}
