using GarmentRecordSystem.Enums;

namespace GarmentRecordSystem.Model;

public class Garment
{
    public uint GarmentId { get; set; }
    public string BrandName { get; set; }
    public DateOnly PurchaseDate { get; set; }
    public string Color { get; set; }
    public Sizes Size { get; set; }
}