using GarmentRecordSystem.Service.Products.Browser;

namespace GarmentRecordSystem.Ui.Factory;

public class AddGarmentUiFactory : UiFactoryBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    
    public AddGarmentUiFactory(IGarmentBrowser garmentBrowser)
    {
        _garmentBrowser = garmentBrowser;
    }

    public override string UiName => "Add";
    
    public override UiBase Create(SortedList<int, UiFactoryBase> factories)
    {
        return new AddGarmentUi(UiName, _garmentBrowser, factories);
    }
}