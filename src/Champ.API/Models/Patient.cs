using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Champ.API.Models;

public class Patient {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long PatientId { get; set; } = 0;

    [Column(TypeName = "mediumblob")]
    [JsonIgnore]
    public byte[] Photo { get; set; } = Array.Empty<byte>();

    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string MiddleName { get; set; } = string.Empty;

    public long PassportNumber { get; set; } = 0;
    
    [Column(TypeName = "TIMESTAMP")]
    public DateTimeOffset DateOfBirth { get; set; } = DateTimeOffset.Now;

    public int GenderId { get; set; }

    [MaxLength(255)]
    public string Address { get; set; } = string.Empty;

    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(255)]
    public string Occupation { get; set; } = string.Empty;
    
    [JsonIgnore]
    public ICollection<Policy> Policies { get; set; } = new List<Policy>();
}