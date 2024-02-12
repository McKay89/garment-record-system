using GarmentRecordSystem.Enums;

namespace GarmentRecordSystem.Model;

public record Garment(uint GarmentId, string BrandName, DateOnly PurchaseDate, string Color, Sizes Size);