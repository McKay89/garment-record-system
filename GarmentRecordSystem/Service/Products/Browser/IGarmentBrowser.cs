using System.Collections;
using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Model;

namespace GarmentRecordSystem.Service.Products.Browser;

public interface IGarmentBrowser
{
    IEnumerable<Garment>? GetAll();
    Garment? SearchGarment(int? id);
    
    void SortByBrandName();
    void SortByPurchaseDate();
    void SortBySize();
    void SortByColor();
    void PrintAllGarment();
    
    bool AddGarment(List<Tuple<string?, string?, Sizes?>> item);
    bool UpdateGarment(Garment? newGarment);
    bool DeleteGarment(int index);
}