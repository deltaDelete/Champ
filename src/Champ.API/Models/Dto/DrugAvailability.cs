namespace Champ.API.Models.Dto;

public class DrugAvailability
{
    public long DrugId { get; set; }
    public int Quantity { get; set; }
    public IEnumerable<Warehouse> Warehouses { get; set; }

    public DrugAvailability(long drugId, int quantity, IEnumerable<Warehouse> warehouses)
    {
        DrugId = drugId;
        Quantity = quantity;
        Warehouses = warehouses;
    }
}