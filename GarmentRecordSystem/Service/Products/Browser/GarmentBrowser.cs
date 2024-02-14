using System.Collections;
using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Utils;

namespace GarmentRecordSystem.Service.Products.Browser;

public class GarmentBrowser : IGarmentBrowser
{
    private List<Garment> _garments;
    private readonly ILogger _logger;
    
    public GarmentBrowser(List<GarmentJson> garments, ILogger logger)
    {
        _garments = garments.Any() ? CreateGarmentList(garments) : new List<Garment>();
        _logger = logger;
    }
    
    public IEnumerable<Garment> GetAll()
    {
        return _garments;
    }

    public Garment? SearchGarment(int? id)
    {
        return _garments.FirstOrDefault(garment => garment.GarmentId == id);
    }

    public bool SortByBrandName()
    {
        if (_garments.Count > 1)
        {
            var sortedGarments = _garments.OrderBy(g => g.BrandName).ToList();
            _garments = sortedGarments;
        
            Changes.IncrementSortCounter();

            return true;
        }
        
        return false;
    }

    public bool SortByPurchaseDate()
    {
        if (_garments.Count > 1)
        {
            var sortedGarments = _garments.OrderBy(g => g.PurchaseDate).ToList();
            _garments = sortedGarments;

            Changes.IncrementSortCounter();

            return true;
        }
        
        return false;
    }

    public bool SortBySize()
    {
        if (_garments.Count > 1)
        {
            var sortedGarments = _garments.OrderBy(g => g.Size).ToList();
            _garments = sortedGarments;

            Changes.IncrementSortCounter();
            
            return true;
        }
        
        return false;
    }

    public bool SortByColor()
    {
        if (_garments.Count > 1)
        {
            var sortedGarments = _garments.OrderBy(g => g.Color).ToList();
            _garments = sortedGarments;

            Changes.IncrementSortCounter();
            
            return true;
        }
        
        return false;
    }

    public bool AddGarment(List<Tuple<string?, string?, Sizes?>> item)
    {
        try
        {
            int maxGarmentId = _garments.Count == 0 ? 0 : _garments.Max(g => g.GarmentId);
            _garments?.Add(new Garment()
            {
                GarmentId = maxGarmentId + 1,
                BrandName = item[0].Item1 ?? "Unknown",
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now),
                Color = item[0].Item2 ?? "Unknown",
                Size = item[0].Item3 ?? (Sizes)Enum.Parse(typeof(Sizes), "S")
            });
            Changes.IncrementAddCounter();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogMessage($"{e}", "ERROR");
            return false;
        }
    }

    public bool UpdateGarment(Garment? newGarment)
    {
        if (newGarment is { GarmentId: 0 }) return false;
        
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

            _logger.LogMessage($"Garment with ID {newGarment.GarmentId} not found.", "ERROR");
            return false;

        }
        catch (Exception e)
        {
            _logger.LogMessage($"{e}", "ERROR");
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

            _logger.LogMessage($"Garment with ID {garmentId} not found.", "ERROR");
            return false;

        }
        catch (Exception e)
        {
            _logger.LogMessage($"{e}", "ERROR");
            return false;
        }
    }

    public void PrintAllGarment()
    {
        Console.WriteLine($" Available garments ({_garments.Count})");
        foreach (var garment in _garments)
        {
            _logger.LogGarment(garment);
        }
    }

    public void PrintGarment(Garment garment)
    {
        _logger.LogGarment(garment);
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