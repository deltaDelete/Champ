using System.ComponentModel.DataAnnotations;

namespace Champ.API.Models;

public class MeasureType {
    public long MeasureTypeId { get; set; }

    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
}