using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Ui;
using GarmentRecordSystem.Controllers;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service;
using GarmentRecordSystem.Service.Products.Browser;

// Create logger
ILogger logger = new ConsoleLogger();

// Read JSON file (every garment is an object)
var garmentsJson = JsonFileReader.Read(@"./garments.json");

// Create Garment Browser instance
IGarmentBrowser garmentBrowser = garmentsJson is { Count: 0 } or null
    ? new GarmentBrowser(new List<GarmentJson>(), logger)
    : new GarmentBrowser(garmentsJson, logger);

// Create UI Factories
UiFactory uiFactory = new UiFactory(garmentBrowser);
var uiFactories = UiFactory.GetFactories();

// Start in Main Menu
Navigator.MainMenu(logger, uiFactories);