using GarmentRecordSystem.Service.Products.Browser;

namespace GarmentRecordSystem.Ui.Factory;

public class UpdateGarmentUiFactory : UiFactoryBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    
    public UpdateGarmentUiFactory(IGarmentBrowser garmentBrowser)
    {
        _garmentBrowser = garmentBrowser;
    }

    public override string UiName => "Update";
    
    public override UiBase Create(SortedList<int, UiFactoryBase> factories)
    {
        return new UpdateGarmentUi(UiName, _garmentBrowser, factories);
    }
}