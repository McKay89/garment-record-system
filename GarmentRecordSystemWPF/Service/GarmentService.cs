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

        public bool AddGarment(List<Tuple<string, DateOnly, string, Sizes>> item)
        {
            try
            {
                int maxGarmentId = _garments.Count == 0 ? 0 : _garments.Max(g => g.GarmentId);
                _garments?.Add(new Garment()
                {
                    GarmentId = maxGarmentId + 1,
                    BrandName = item[0].Item1,
                    PurchaseDate = item[0].Item2,
                    Color = item[0].Item3,
                    Size = item[0].Item4
                });
                Changes.IncrementAddCounter();
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return false;
            }
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

        public bool UpdateGarment(Garment newGarment)
        {
            try
            {
                var existingGarment = _garments.FirstOrDefault(g => g.GarmentId == newGarment.GarmentId);

                if (existingGarment != null)
                {
                    int index = _garments.IndexOf(existingGarment);
                    _garments[index] = newGarment;
                    Changes.IncrementUpdateCounter();
                    return true;
                }

                return false;

            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return false;
            }
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
