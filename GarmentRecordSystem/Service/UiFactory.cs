using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Ui.Factory;

namespace GarmentRecordSystem.Service;

public class UiFactory
{
    private static IGarmentBrowser? _garmentBrowser;
    
    public UiFactory(IGarmentBrowser garmentBrowser)
    {
        _garmentBrowser = garmentBrowser;
    }

    public static SortedList<int, UiFactoryBase> GetFactories()
    {
        UiFactoryBase addGarmentUiFactory = new AddGarmentUiFactory(_garmentBrowser);
        UiFactoryBase updateGarmentUiFactory = new UpdateGarmentUiFactory(_garmentBrowser);
    
        var factories = new SortedList<int, UiFactoryBase>
        {
            { 1, addGarmentUiFactory },           // UI for creating new garment
            { 2, updateGarmentUiFactory }         // UI for updating an existing garment
        };

        return factories;
    }
}