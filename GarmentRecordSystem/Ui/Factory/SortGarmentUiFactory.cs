using GarmentRecordSystem.Service.Products.Browser;

namespace GarmentRecordSystem.Ui.Factory;

public class SortGarmentUiFactory : UiFactoryBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    
    public SortGarmentUiFactory(IGarmentBrowser garmentBrowser)
    {
        _garmentBrowser = garmentBrowser;
    }

    public override string UiName => "Sort";
    
    public override UiBase Create(SortedList<int, UiFactoryBase> factories)
    {
        return new SortGarmentUi(UiName, _garmentBrowser, factories);
    }
}