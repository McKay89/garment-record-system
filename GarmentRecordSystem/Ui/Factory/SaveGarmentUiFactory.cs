using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Utils;

namespace GarmentRecordSystem.Ui.Factory;

public class SaveGarmentUiFactory : UiFactoryBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    
    public SaveGarmentUiFactory(IGarmentBrowser garmentBrowser)
    {
        _garmentBrowser = garmentBrowser;
    }

    public override string UiName => $"Save {(Changes.GetChangeSum() == 0 
        ? "( no changes )" 
        : $"( {Changes.GetChangeSum()} changes are not saved )")}";
    
    private static string UiName2 => "Save";
    
    public override UiBase Create(SortedList<int, UiFactoryBase> factories)
    {
        return new SaveGarmentUi(UiName2, _garmentBrowser, factories);
    }
}