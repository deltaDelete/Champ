using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class Patient {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PatientId { get; set; } = 0;

    public byte[] Photo { get; set; } = Array.Empty<byte>();

    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string MiddleName { get; set; } = string.Empty;

    public long PassportNumber { get; set; } = 0;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [DefaultValue("CURRENT_DATE")]
    public DateOnly DateOfBirth { get; set; }

    [ForeignKey(nameof(Gender))]
    public int GenderId { get; set; }

    public Gender? Gender { get; set; }

    [MaxLength(255)]
    public string Address { get; set; } = string.Empty;

    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}