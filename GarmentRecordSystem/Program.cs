using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Ui;
using GarmentRecordSystem.Controllers;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Ui.Factory;
using GarmentRecordSystem.Ui.Selector;

// Create logger
ILogger logger = new ConsoleLogger();

// Read JSON file (every garment is an object)
var garmentsJson = JsonFileReader.Read(@"./garments.json");


IGarmentBrowser garmentBrowser = garmentsJson is { Count: 0 }
    ? new GarmentBrowser(null, logger)
    : new GarmentBrowser(garmentsJson, logger);

// Create UI Factory Bases
UiFactoryBase addGarmentUiFactory = new AddGarmentUiFactory(garmentBrowser);

// Create factory list
SortedList<int, UiFactoryBase> factories = new SortedList<int, UiFactoryBase>
{
    { 1, addGarmentUiFactory }      // UI for creating new garment
};

// Create UI Selector
var uiSelector = new UiSelector(logger, factories);
UiBase ui = uiSelector.Select();

ui.DisplayTitle();
ui.PrintSubmenuInfos();
ui.Run();