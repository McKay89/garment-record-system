using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Ui;
using GarmentRecordSystem.Controllers;
using GarmentRecordSystem.Model;
using GarmentRecordSystem.Service;
using GarmentRecordSystem.Service.Products.Browser;

// Create logger
ILogger logger = new ConsoleLogger();

// Read JSON file (every garment is an object)
var garmentsJson = JsonFileReader.Read();

// Handle if garmentJson is NULL
if (garmentsJson == null)
{
    logger.LogMessage("An error occured while opening JSON file. Application is shutting down...", "ERROR");
    Thread.Sleep(2000);
    Environment.Exit(0);
}

// Create Garment Browser instance
IGarmentBrowser garmentBrowser = new GarmentBrowser(garmentsJson, logger);

// Create UI Factories
UiFactory uiFactory = new UiFactory(garmentBrowser);
var uiFactories = UiFactory.GetFactories();

// Start in Main Menu
Navigator.MainMenu(logger, uiFactories);