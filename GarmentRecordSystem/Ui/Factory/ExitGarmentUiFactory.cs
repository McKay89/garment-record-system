using GarmentRecordSystem.Service.Products.Browser;

namespace GarmentRecordSystem.Ui.Factory;

public class ExitGarmentUiFactory : UiFactoryBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    
    public ExitGarmentUiFactory(IGarmentBrowser garmentBrowser)
    {
        _garmentBrowser = garmentBrowser;
    }

    public override string UiName => "Exit";
    
    public override UiBase Create(SortedList<int, UiFactoryBase> factories)
    {
        return new ExitGarmentUi(UiName, _garmentBrowser, factories);
    }
}