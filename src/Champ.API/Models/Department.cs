using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class Department {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long DepartmentId { get; set; } = 0;
    [MaxLength(255)]
    public string DepartmentName { get; set; } = string.Empty;
}