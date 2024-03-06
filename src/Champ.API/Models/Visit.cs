using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

public class Visit {
    public int VisitId { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }

    public Patient? Patient { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [DefaultValue("CURRENT_DATE")]
    public DateTimeOffset Date { get; set; }
}