using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service.Logger;

namespace GarmentRecordSystem.Service.Products.Browser;

public class GarmentBrowser : IGarmentBrowser
{
    private IList<Garment> _garments;
    private readonly ILogger _logger;
    public int ChangesCounter { get; private set; } = 0;
    
    public GarmentBrowser(IList<GarmentJson>? garments, ILogger logger)
    {
        _garments = garments != null ? CreateGarmentList(garments) : new List<Garment>();
        _logger = logger;
    }
    
    public IEnumerable<Garment> GetAll()
    {
        return _garments;
    }

    public Garment? SearchGarment(int id)
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

    public bool AddGarment(Garment item)
    {
        try
        {
            _garments?.Add(item);
            ChangesCounter++;
            return true;
        }
        catch (Exception e)
        {
            _logger.LogMessage($"{e}", "ERROR");
            return false;
        }
    }

    public bool UpdateGarment(int index, Garment newGarment)
    {
        try
        {
            if (index >= 0 && index < _garments.Count)
            {
                _garments[index] = newGarment;
                ChangesCounter++;
                return true;
            }

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

    private static IList<Garment> CreateGarmentList(IList<GarmentJson> garments)
    {
        return (IList<Garment>)garments.Select(garment => new Garment
        {
            GarmentId = garment.GarmentId,
            BrandName = garment.BrandName,
            PurchaseDate = DateOnly.Parse(garment.PurchaseDate),
            Color = garment.Color,
            Size = (Sizes)Enum.Parse(typeof(Sizes), garment.Size)
        });
    }
}