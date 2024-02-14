using GarmentRecordSystemWPF.Enums;
using GarmentRecordSystemWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarmentRecordSystemWPF.Service
{
    internal interface IGarmentService
    {
        IEnumerable<Garment>? GetAll();
        List<Garment> SearchGarment(int id);

        IEnumerable<Garment>? SortByBrandName();
        IEnumerable<Garment>? SortByPurchaseDate();
        IEnumerable<Garment>? SortBySize();
        IEnumerable<Garment>? SortByColor();

        bool AddGarment(List<Tuple<string?, string?, Sizes?>> item);
        bool UpdateGarment(Garment? newGarment);
        bool DeleteGarment(int? garmentId);
    }
}
