using System.Collections;
using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service.Logger;

namespace GarmentRecordSystem.Service.Products.Browser;

public class GarmentBrowser : IGarmentBrowser
{
    private List<Garment> _garments;
    private readonly ILogger _logger;
    public int ChangesCounter { get; private set; } = 0;
    
    public GarmentBrowser(List<GarmentJson>? garments, ILogger logger)
    {
        _garments = garments != null ? CreateGarmentList(garments) : new List<Garment>();
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

    public void SortByBrandName()
    {
        var sortedGarments = _garments.OrderBy(g => g.BrandName).ToList();
        _garments = sortedGarments;
    }

    public void SortByPurchaseDate()
    {
        var sortedGarments = _garments.OrderBy(g => g.PurchaseDate).ToList();
        _garments = sortedGarments;
    }

    public void SortBySize()
    {
        var sortedGarments = _garments.OrderBy(g => g.Size).ToList();
        _garments = sortedGarments;
    }

    public void SortByColor()
    {
        var sortedGarments = _garments.OrderBy(g => g.Color).ToList();
        _garments = sortedGarments;
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
            ChangesCounter++;
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
                ChangesCounter++;
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

    public bool DeleteGarment(int index)
    {
        try
        {
            _garments.RemoveAt(index);
            ChangesCounter++;
            return true;
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