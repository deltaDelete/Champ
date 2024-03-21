using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Champ.API.Models;

/// <summary>
/// Лекарство
/// Предоставляет общую информацию о лекарственном препарате
/// </summary>
public class Drug {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long DrugId { get; set; } = 0;

    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(4096)]
    public string Description { get; set; } = string.Empty;

    [Precision(8, 2)]
    public decimal Price { get; set; }

    [JsonIgnore]
    public IList<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
