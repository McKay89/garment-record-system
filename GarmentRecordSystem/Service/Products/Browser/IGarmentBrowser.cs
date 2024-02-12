using GarmentRecordSystem.Model;

namespace GarmentRecordSystem.Service.Products.Browser;

public interface IGarmentBrowser
{
    IEnumerable<Garment> GetAll();
    
    IEnumerable<Garment> SortByBrandName();
    IEnumerable<Garment> SortByPurchaseDate();
    IEnumerable<Garment> SortBySize();
    IEnumerable<Garment> SortByColor();
    
    bool SetNewItem(object item);
    bool UpdateItem(uint id);
    bool DeleteItem(uint id);
}