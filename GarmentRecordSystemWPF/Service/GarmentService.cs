using GarmentRecordSystemWPF.Enums;
using GarmentRecordSystemWPF.Model;
using GarmentRecordSystemWPF.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (garmentId == null) return false;

            try
            {
                var existingGarment = _garments.FirstOrDefault(g => g.GarmentId == garmentId);

                if (existingGarment != null)
                {
                    int index = _garments.IndexOf(existingGarment);
                    _garments.RemoveAt(index);

                    Changes.IncrementDeleteCounter();
                    return true;
                }

                return false;

            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                return false;
            }
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

        public List<Garment> SortByGarmentId()
        {
            if (_garments.Count > 1)
            {
                _garments = _garments.OrderBy(g => g.GarmentId).ToList();

                Changes.IncrementSortCounter();
            }

            return _garments;
        }

        public List<Garment> SortByBrandName()
        {
            if (_garments.Count > 1)
            {
                _garments = _garments.OrderBy(g => g.BrandName).ToList();

                Changes.IncrementSortCounter();
            }

            return _garments;
        }

        public List<Garment> SortByColor()
        {
            if (_garments.Count > 1)
            {
                _garments = _garments.OrderBy(g => g.Color).ToList();

                Changes.IncrementSortCounter();
            }

            return _garments;
        }

        public List<Garment> SortByPurchaseDate()
        {
            if (_garments.Count > 1)
            {
                _garments = _garments.OrderBy(g => g.PurchaseDate).ToList();

                Changes.IncrementSortCounter();
            }

            return _garments;
        }

        public List<Garment> SortBySize()
        {
            if (_garments.Count > 1)
            {
                _garments = _garments.OrderBy(g => g.Size).ToList();

                Changes.IncrementSortCounter();
            }

            return _garments;
        }

        public bool UpdateGarment(Garment? newGarment)
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
