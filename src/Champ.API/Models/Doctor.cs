using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class Doctor {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DoctorId { get; set; } = 0;

    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string MiddleName { get; set; } = string.Empty;
}