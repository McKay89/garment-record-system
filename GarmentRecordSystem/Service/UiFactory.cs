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
        UiFactoryBase deleteGarmentUiFactory = new DeleteGarmentUiFactory(_garmentBrowser);
        UiFactoryBase searchGarmentUiFactory = new SearchGarmentUiFactory(_garmentBrowser);
        UiFactoryBase sortGarmentUiFactory = new SortGarmentUiFactory(_garmentBrowser);
    
        var factories = new SortedList<int, UiFactoryBase>
        {
            { 1, addGarmentUiFactory },           // UI for creating new garment
            { 2, updateGarmentUiFactory },        // UI for updating an existing garment
            { 3, deleteGarmentUiFactory },        // UI for deleting an existing garment
            { 4, searchGarmentUiFactory },        // UI for search a garment
            { 5, sortGarmentUiFactory }           // UI for sorting garment list
        };

        return factories;
    }
}