using GarmentRecordSystem.Service.Products.Browser;

namespace GarmentRecordSystem.Ui.Factory;

public class SearchGarmentUiFactory : UiFactoryBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    
    public SearchGarmentUiFactory(IGarmentBrowser garmentBrowser)
    {
        _garmentBrowser = garmentBrowser;
    }

    public override string UiName => "Search";
    
    public override UiBase Create(SortedList<int, UiFactoryBase> factories)
    {
        return new SearchGarmentUi(UiName, _garmentBrowser, factories);
    }
}