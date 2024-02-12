using GarmentRecordSystem.Model;

namespace GarmentRecordSystem.Service.Products.Browser;

public class GarmentBrowser : IGarmentBrowser
{
    private IEnumerable<GarmentJson>? _garments;
    public GarmentBrowser(IReadOnlyCollection<GarmentJson>? garments)
    {
        if (garments != null) _garments = garments;
    }
    
    public IEnumerable<Garment> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Garment> SortByBrandName()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Garment> SortByPurchaseDate()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Garment> SortBySize()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Garment> SortByColor()
    {
        throw new NotImplementedException();
    }

    public bool SetNewItem(object item)
    {
        throw new NotImplementedException();
    }

    public bool UpdateItem(uint id)
    {
        throw new NotImplementedException();
    }

    public bool DeleteItem(uint id)
    {
        throw new NotImplementedException();
    }
}