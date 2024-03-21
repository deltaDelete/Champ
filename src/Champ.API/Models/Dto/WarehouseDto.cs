namespace Champ.API.Models.Dto;

public class WarehouseDto {
    public long WarehouseId { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;

    public WarehouseDto(Warehouse warehouse, int quantity) {
        WarehouseId = warehouse.WarehouseId;
        Name = warehouse.Name;
        Quantity = quantity;
    }
}
