using System.Collections;
using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Model;

namespace GarmentRecordSystem.Service.Products.Browser;

public interface IGarmentBrowser
{
    IEnumerable<Garment>? GetAll();
    Garment? SearchGarment(int? id);
    
    bool SortByBrandName();
    bool SortByPurchaseDate();
    bool SortBySize();
    bool SortByColor();
    void PrintAllGarment();
    void PrintGarment(Garment garment);
    
    bool AddGarment(List<Tuple<string?, string?, Sizes?>> item);
    bool UpdateGarment(Garment? newGarment);
    bool DeleteGarment(int? garmentId);
}