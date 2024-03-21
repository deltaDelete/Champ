using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Champ.API.Models;

public class Warehouse {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long WarehouseId { get; set; } = 0;

    /// <summary>
    /// Название склада, для удобной идентификации
    /// </summary>
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public IList<Drug> Drugs { get; set; } = new List<Drug>();
}
