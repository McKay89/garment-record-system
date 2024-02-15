using GarmentRecordSystem.Enums;
using GarmentRecordSystem.Service.Logger;
using GarmentRecordSystem.Service.Products.Browser;
using GarmentRecordSystem.Ui.Factory;
using GarmentRecordSystem.Utils;

namespace GarmentRecordSystem.Ui;

public class AddGarmentUi : UiBase
{
    private readonly IGarmentBrowser _garmentBrowser;
    private readonly ILogger _logger = new ConsoleLogger();
    private readonly SortedList<int, UiFactoryBase> _factories;
    
    public AddGarmentUi(string title, IGarmentBrowser garmentBrowser, SortedList<int, UiFactoryBase> factories) : base(title)
    {
        _garmentBrowser = garmentBrowser;
        _factories = factories;
    }

    public override void Run()
    {
        var collectedData = CollectData();
        bool result = _garmentBrowser.AddGarment(collectedData);

        if (result)
        {
            _garmentBrowser.PrintAllGarment();
            _logger.LogMessage("Garment successfully created! Return to Main Menu...", "SUCCESS");
        }
        else
        {
            _logger.LogMessage("An error occured! Return to Main Menu...", "ERROR");
        }
        
        Thread.Sleep(2000);
        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        Navigator.MainMenu(_logger, _factories);
    }

    private List<Tuple<string?, DateOnly?, string?, Sizes?>> CollectData()
    {
        // Get Brand Name
        var brand = "";
        do
        {
            Console.Write(" Brand name: ");
            brand = Console.ReadLine();
        } while (InputValidator.Null(brand));
        if (brand == "--back") ReturnToMainMenu();
        
        // Get Purchase Date
        var date = InputValidator.Date(" Purchase Date: ");
        if (date == null)
        {
            ReturnToMainMenu();
        }
        
        // Get Color
        var color = "";
        do
        {
            Console.Write(" Color: ");
            color = Console.ReadLine();
        } while (InputValidator.Null(color));
        if (color == "--back") ReturnToMainMenu();
        
        // Get Size
        var size = InputValidator.Size(" Size: ");
        if (size == null)
        {
            ReturnToMainMenu();
        }

        var collectedData = new List<Tuple<string?, DateOnly?, string?, Sizes?>>
        {
            Tuple.Create(brand, date, color, size)!
        };

        return collectedData;
    }
}