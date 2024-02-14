using GarmentRecordSystemWPF.Enums;
using GarmentRecordSystemWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarmentRecordSystemWPF.Service
{
    internal class GarmentService : IGarmentService
    {
        private List<Garment> _garments;

        public GarmentService(List<GarmentJson> garments)
        {
            _garments = garments.Any() ? CreateGarmentList(garments) : new List<Garment>();
        }

        public bool AddGarment(List<Tuple<string?, string?, Sizes?>> item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGarment(int? garmentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Garment>? GetAll()
        {
            return _garments;
        }

        public List<Garment> SearchGarment(int id)
        {
            var foundGarment = new List<Garment>();
            Garment? found = _garments.FirstOrDefault(garment => garment.GarmentId == id);
        
            return found != null ? new List<Garment>() { found } : new List<Garment>();
        }

        IEnumerable<Garment>? IGarmentService.SortByBrandName()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Garment>? IGarmentService.SortByColor()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Garment>? IGarmentService.SortByPurchaseDate()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Garment>? IGarmentService.SortBySize()
        {
            throw new NotImplementedException();
        }

        bool IGarmentService.UpdateGarment(Garment? newGarment)
        {
            throw new NotImplementedException();
        }

        private static List<Garment> CreateGarmentList(List<GarmentJson> garments)
        {
            return garments.Select(garment => new Garment()
            {
                GarmentId = garment.GarmentId,
                BrandName = garment.BrandName,
                PurchaseDate = DateOnly.Parse(garment.PurchaseDate),
                Color = garment.Color,
                Size = (Sizes)Enum.Parse(typeof(Sizes), garment.Size)
            })
                .ToList();
        }
    }
}
