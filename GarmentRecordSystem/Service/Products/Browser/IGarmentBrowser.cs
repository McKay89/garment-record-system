using GarmentRecordSystem.Model;

namespace GarmentRecordSystem.Service.Products.Browser;

public interface IGarmentBrowser
{
    IEnumerable<Garment>? GetAll();
    Garment? SearchGarment(int id);
    
    void SortByBrandName();
    void SortByPurchaseDate();
    void SortBySize();
    void SortByColor();
    
    bool AddGarment(Garment item);
    bool UpdateGarment(int index, Garment newGarment);
    bool DeleteGarment(int index);
}