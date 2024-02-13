using GarmentRecordSystem.Service.Products.Browser;

namespace GarmentRecordSystem.Ui.Factory;

public class DeleteGarmentUiFactory : UiFactoryBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    
    public DeleteGarmentUiFactory(IGarmentBrowser garmentBrowser)
    {
        _garmentBrowser = garmentBrowser;
    }

    public override string UiName => "Delete";
    
    public override UiBase Create(SortedList<int, UiFactoryBase> factories)
    {
        return new DeleteGarmentUi(UiName, _garmentBrowser, factories);
    }
}