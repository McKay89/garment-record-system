using GarmentRecordSystemWPF.Enums;

namespace GarmentRecordSystemWPF.Model
{
    internal class Garment
    {
        public int GarmentId { get; init; }
        public string BrandName { get; init; }
        public DateOnly PurchaseDate { get; init; }
        public string Color { get; init; }
        public Sizes Size { get; init; }
    }
}
