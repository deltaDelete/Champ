using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class ProcedureType {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProcedureTypeId { get; set; } = 0;

    [MaxLength(50)]
    public string ProcedureTypeName { get; set; } = string.Empty;
}