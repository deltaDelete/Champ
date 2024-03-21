using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Champ.API.Models;

/// <summary>
/// Партия лекарств
/// </summary>
public class DrugStock {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long DrugStockId { get; set; }

    [ForeignKey(nameof(Drug))]
    public long DrugId { get; set; }

    [ForeignKey(nameof(Warehouse))]
    public long WarehouseId { get; set; }

    /// <summary>
    /// Количество товаров в партии
    /// </summary>
    public int Quantity { get; set; } = 0;

    /// <summary>
    /// Лекарство этой партии
    /// </summary>
    public Drug? Drug { get; set; }

    /// <summary>
    /// Склад, на котором находится партия
    /// </summary>
    public Warehouse? Warehouse { get; set; }

    /// <summary>
    /// Дата поступления
    /// </summary>
    public DateTimeOffset ReceiptDate { get; set; }

    /// <summary>
    /// Срок годности
    /// </summary>
    public DateTimeOffset ExpirationDate { get; set; }
}
